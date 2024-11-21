using System;

namespace Modules.Gardening
{
    public class SeedbedImpl : ISeedbed
    {
        private const int SlopsValue = 1;
        public event Action<bool> OnHarvestWateringRequired;
        public event Action<HarvestState> OnHarvestStateChanged;
        public event Action<HarvestAge> OnHarvestAgeChanged;
        public event Action<float> OnHarvestProgressChanged;
        public event Action OnGathered;
        public event Action<float> OnDryingTimerProgressChanged;

        private IHarvest _harvest;
        private bool _isEnable;

        public IHarvest Harvest => _harvest; 

        public bool Seed(PlantConfig plant) 
        {
            if (_harvest is not null) return false;

            _harvest = new Harvest(plant);
            _harvest.OnStateChanged += OnStateChanged;
            _harvest.OnAgeChanged += OnAgeChanged;
            _harvest.OnWaterRequired += OnWateringRequired;
            _harvest.OnProgressChanged += OnProgressChanged;
            _harvest.OnDryingTimerProgressChanged += OnDryingProgressChanged;
            _harvest.StartGrow();

            _isEnable = true;

            return true;
        }

        public bool Gather(out HarvestResult harvestResult) 
        {
            harvestResult = new HarvestResult();
            if (_harvest is null || _harvest.State == HarvestState.Growing) return false;

            harvestResult.IsNormal = _harvest.State == HarvestState.Ready;
            harvestResult.Value = harvestResult.IsNormal ? _harvest.Value : SlopsValue;
            harvestResult.Plant = _harvest.PlantConfig.Plant;
            
            StopGrow();
            OnGathered?.Invoke();

            return true;
        }

        public void Watering()
        {
            _harvest?.Watering();
            OnHarvestWateringRequired?.Invoke(false);
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnable) return;
            
            _harvest?.Tick(deltaTime);
        }

        public void Pause() => _isEnable = false;

        public void Resume() => _isEnable = true;

        public void Stop() => StopGrow();

        private void StopGrow()
        {
            _isEnable = false;
            if (_harvest is null) return;

            _harvest.OnStateChanged -= OnStateChanged;
            _harvest.OnAgeChanged -= OnAgeChanged;
            _harvest.OnWaterRequired -= OnWateringRequired;
            _harvest.OnProgressChanged -= OnProgressChanged;
            _harvest.OnDryingTimerProgressChanged -= OnDryingProgressChanged;
            _harvest.StopGrow();
            
            _harvest = null;
        }

        private void OnAgeChanged(HarvestAge age) => OnHarvestAgeChanged?.Invoke(age);

        private void OnStateChanged(HarvestState state) => OnHarvestStateChanged?.Invoke(state);

        private void OnWateringRequired() => OnHarvestWateringRequired?.Invoke(true);

        private void OnProgressChanged(float progress) => OnHarvestProgressChanged?.Invoke(progress);

        private void OnDryingProgressChanged(float progress) => OnDryingTimerProgressChanged?.Invoke(progress);
    }
}