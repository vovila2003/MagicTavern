using Modules.Gardening;
using UnityEngine;

namespace Tavern.Gardening
{
    public class HarvestViewController
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly GameObject _waterIndicator;
        private readonly ISeedbed _seedbed;

        private PlantMetadata _metadata;

        public HarvestViewController(ISeedbed seedbed,
            SpriteRenderer spriteRenderer, GameObject waterIndicator) 
        {
            _seedbed = seedbed;
            _spriteRenderer = spriteRenderer;
            _waterIndicator = waterIndicator;
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnHarvestAgeChanged += OnHarvestAgeChanged;
            _seedbed.OnHarvestWateringRequired += OnHarvestWateringRequired;
            _seedbed.OnGathered += OnGathered;
        }

        public void Dispose()
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
                _spriteRenderer.sprite = _metadata.Drying[age];
                _waterIndicator.SetActive(false);
            }
        }

        private void OnHarvestAgeChanged(HarvestAge harvestAge)
        {
            Debug.Log($"Harvest age changed to {harvestAge}");

            _metadata ??= _seedbed.Harvest.PlantConfig.PlantMetadata;
            
            _spriteRenderer.sprite = _metadata.Healthy[(int) harvestAge];
        }

        private void OnHarvestWateringRequired(bool isNeed)
        {
            _waterIndicator.SetActive(isNeed);
        }

        private void OnGathered()
        {
            _metadata = null;
            _spriteRenderer.sprite = null;
            _waterIndicator.SetActive(false);
        }
    }
}