using System;

namespace Modules.Gardening
{
    public class SeedbedImpl : ISeedbed
    {
        private const int SlopsValue = 1;
        public event Action<bool> OnHarvestWateringRequired;

        public event Action<bool> OnHealingRequired;
        public event Action<HarvestState> OnHarvestStateChanged;
        public event Action<HarvestAge> OnHarvestAgeChanged;
        public event Action<float> OnHarvestProgressChanged;
        public event Action OnGathered;
        public event Action<float> OnDryingTimerProgressChanged;

        private bool _isEnable;

        public IHarvest Harvest { get; private set; }
        public SeedbedBoost SeedbedBoost { get; } = new();

        public bool Seed(PlantConfig plant) 
        {
            if (Harvest is not null) return false;

            Harvest = new Harvest(plant, SeedbedBoost);
            Harvest.OnStateChanged += OnStateChanged;
            Harvest.OnAgeChanged += OnAgeChanged;
            Harvest.OnWaterRequired += OnWateringRequired;
            Harvest.OnProgressChanged += OnProgressChanged;
            Harvest.OnDryingTimerProgressChanged += OnDryingProgressChanged;
            Harvest.StartGrow();

            _isEnable = true;

            return true;
        }

        public bool Gather(out HarvestResult harvestResult) 
        {
            harvestResult = new HarvestResult();
            if (Harvest is null || Harvest.State == HarvestState.Growing) return false;

            harvestResult.IsNormal = Harvest.State == HarvestState.Ready;
            harvestResult.Value = harvestResult.IsNormal 
                ? (int) (Harvest.Value * (1 + SeedbedBoost.HarvestBoostInPercent / 100.0f)) 
                : SlopsValue;
            harvestResult.Plant = Harvest.PlantConfig.Plant;
            
            StopGrow();
            OnGathered?.Invoke();

            return true;
        }

        public void Watering()
        {
            if (Harvest is null) return;
            
            Harvest.Watering();
            OnHarvestWateringRequired?.Invoke(false);
        }

        public void Heal()
        {
            if (Harvest is null) return;
            
            Harvest.Heal();
            OnHealingRequired?.Invoke(false);
        }

        public void ReduceHarvestSicknessProbability(int reducing)
        {
            Harvest?.ReduceHarvestSicknessProbability(reducing);
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnable) return;
            
            Harvest?.Tick(deltaTime);
        }

        public void Pause() => _isEnable = false;

        public void Resume() => _isEnable = true;

        public void Stop() => StopGrow();

        private void StopGrow()
        {
            _isEnable = false;
            if (Harvest is null) return;

            Harvest.OnStateChanged -= OnStateChanged;
            Harvest.OnAgeChanged -= OnAgeChanged;
            Harvest.OnWaterRequired -= OnWateringRequired;
            Harvest.OnProgressChanged -= OnProgressChanged;
            Harvest.OnDryingTimerProgressChanged -= OnDryingProgressChanged;
            Harvest.StopGrow();
            
            Harvest = null;
        }

        private void OnAgeChanged(HarvestAge age) => OnHarvestAgeChanged?.Invoke(age);

        private void OnStateChanged(HarvestState state) => OnHarvestStateChanged?.Invoke(state);

        private void OnWateringRequired() => OnHarvestWateringRequired?.Invoke(true);

        private void OnProgressChanged(float progress) => OnHarvestProgressChanged?.Invoke(progress);

        private void OnDryingProgressChanged(float progress) => OnDryingTimerProgressChanged?.Invoke(progress);
    }
}