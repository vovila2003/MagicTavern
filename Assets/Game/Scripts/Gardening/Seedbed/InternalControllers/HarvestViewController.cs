using Modules.Gardening;
using UnityEngine;

namespace Tavern.Gardening
{
    public class HarvestViewController : MonoBehaviour
    {
        [SerializeField]
        private Seedbed Seedbed;
        
        [SerializeField] 
        private SpriteRenderer HarvestSpriteRenderer;

        [SerializeField] 
        private GameObject WaterIndicator;
        
        private ISeedbed _seedbed;
        private PlantMetadata _metadata;

        private void Awake()
        {
            _seedbed = Seedbed.SeedbedImpl;
        }

        private void OnEnable()
        {
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnHarvestAgeChanged += OnHarvestAgeChanged;
            _seedbed.OnHarvestWateringRequired += OnHarvestWateringRequired;
            _seedbed.OnGathered += OnGathered;
        }

        private void OnDisable()
        {
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnHarvestAgeChanged -= OnHarvestAgeChanged;
            _seedbed.OnHarvestWateringRequired -= OnHarvestWateringRequired;
            _seedbed.OnGathered -= OnGathered;
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            if (state == HarvestState.Dried)
            {
                var age = (int) _seedbed.Harvest.Age;
                HarvestSpriteRenderer.sprite = _metadata.Drying[age];
                WaterIndicator.SetActive(false);
            }
        }

        private void OnHarvestAgeChanged(HarvestAge harvestAge)
        {
            Debug.Log($"Harvest age changed to {harvestAge}");

            _metadata ??= _seedbed.Harvest.PlantConfig.PlantMetadata;
            
            HarvestSpriteRenderer.sprite = _metadata.Healthy[(int) harvestAge];
        }

        private void OnHarvestWateringRequired(bool isNeed)
        {
            WaterIndicator.SetActive(isNeed);
        }

        private void OnGathered()
        {
            _metadata = null;
            HarvestSpriteRenderer.sprite = null;
            WaterIndicator.SetActive(false);
        }
    }
}