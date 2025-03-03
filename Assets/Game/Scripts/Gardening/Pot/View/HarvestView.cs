using Modules.Gardening;
using UnityEngine;

namespace Tavern.Gardening
{
    public class HarvestView : MonoBehaviour
    {
        [SerializeField]
        private Pot Pot;
        
        [SerializeField] 
        private SpriteRenderer HarvestSpriteRenderer;

        [SerializeField] 
        private GameObject WaterIndicator;

        [SerializeField] 
        private GameObject ReadyIndicator;

        private ISeedbed _seedbed;
        private PlantMetadata _metadata;
        public Sprite CurrentSprite { get; private set; }

        private void Awake()
        {
            _seedbed = Pot.Seedbed;
        }

        private void OnEnable()
        {
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnHarvestAgeChanged += OnHarvestAgeChanged;
            _seedbed.OnHarvestWateringRequired += OnHarvestWateringRequired;
            _seedbed.OnHealingRequired += OnHarvestHealingRequired;
            _seedbed.OnGathered += OnGathered;
        }

        private void OnDisable()
        {
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnHarvestAgeChanged -= OnHarvestAgeChanged;
            _seedbed.OnHarvestWateringRequired -= OnHarvestWateringRequired;
            _seedbed.OnHealingRequired -= OnHarvestHealingRequired;
            _seedbed.OnGathered -= OnGathered;
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            Debug.Log($"Harvest state changed to {state}");
            
            ReadyIndicator.SetActive(state != HarvestState.Growing);

            if (state != HarvestState.Dried) return;
            
            var age = (int) _seedbed.Harvest.Age;
            CurrentSprite = _metadata.Drying[age];
            HarvestSpriteRenderer.sprite = CurrentSprite;
            WaterIndicator.SetActive(false);
        }

        private void OnHarvestAgeChanged(HarvestAge harvestAge)
        {
            Debug.Log($"Harvest age changed to {harvestAge}");

            _metadata ??= _seedbed.Harvest.PlantConfig.PlantMetadata;

            CurrentSprite = _seedbed.Harvest.IsSick 
                ? _metadata.Sick[(int) harvestAge]                
                : _metadata.Healthy[(int) harvestAge];
            
            HarvestSpriteRenderer.sprite = CurrentSprite;
        }

        private void OnHarvestWateringRequired(bool isRequired)
        {
            Debug.Log($"Water required changed to {isRequired}");
            
            WaterIndicator.SetActive(isRequired);
        }

        private void OnHarvestHealingRequired(bool isSick)
        {
            Debug.Log($"Sickness changed to {isSick}");
            
            var age = (int) _seedbed.Harvest.Age;
            CurrentSprite = isSick? _metadata.Drying[age] : _metadata.Healthy[age];
            HarvestSpriteRenderer.sprite = CurrentSprite;
        }

        private void OnGathered()
        {
            _metadata = null;
            HarvestSpriteRenderer.sprite = null;
            CurrentSprite = null;
            WaterIndicator.SetActive(false);
            ReadyIndicator.SetActive(false);
        }
    }
}