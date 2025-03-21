using System;

namespace Modules.Gardening
{
    public class Seedbed : ISeedbed
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
        // public bool IsFertilized => _isBoosted || _isSickReduced || _isAccelerated;
        public bool IsFertilized => IsBoosted || IsAccelerated;
        public bool IsBoosted { get; set; }
        public bool IsSickReduced { get; set; }
        public bool IsAccelerated { get; set; }
        public int SeedInHarvestProbability { get; set; }

        public bool Seed(PlantConfig plant) 
        {
            if (Harvest is not null) return false;

            Harvest = new Harvest(plant);
            Harvest.OnStateChanged += OnStateChanged;
            Harvest.OnAgeChanged += OnAgeChanged;
            Harvest.OnWaterRequired += OnWateringRequired;
            Harvest.OnProgressChanged += OnProgressChanged;
            Harvest.OnDryingTimerProgressChanged += OnDryingProgressChanged;
            Harvest.StartGrow();

            _isEnable = true;
            IsBoosted = false;
            IsSickReduced = false;
            IsAccelerated = false;
            SeedInHarvestProbability = 0;

            return true;
        }

        public bool Gather(out HarvestResult harvestResult) 
        {
            if (!CalculateHarvest(out harvestResult)) return false;

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

        public bool ReduceHarvestSicknessProbability(int reducing)
        {
            if (IsSickReduced) return false;
            
            IsSickReduced = true;
            Harvest?.ReduceHarvestSicknessProbability(reducing);

            return true;
        }

        public bool BoostHarvestAmount(int boostInPercent)
        {
            if (IsBoosted) return false;
            
            IsBoosted = true;
            Harvest?.BoostHarvestAmount(boostInPercent);

            return true;
        }

        public bool AccelerateGrowth(int accelerationInPercent)
        {
            if (IsAccelerated) return false;
            
            IsAccelerated = true;
            Harvest?.AccelerateGrowth(accelerationInPercent);

            return true;
        }

        public void SetSeedInHarvestProbability(int probability) => SeedInHarvestProbability = probability;

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

        private void OnAgeChanged(HarvestAge age, bool _) => OnHarvestAgeChanged?.Invoke(age);

        private void OnStateChanged(HarvestState state) => OnHarvestStateChanged?.Invoke(state);

        private void OnWateringRequired() => OnHarvestWateringRequired?.Invoke(true);

        private void OnProgressChanged(float progress) => OnHarvestProgressChanged?.Invoke(progress);

        private void OnDryingProgressChanged(float progress) => OnDryingTimerProgressChanged?.Invoke(progress);

        private bool CalculateHarvest(out HarvestResult harvestResult)
        {
            harvestResult = new HarvestResult();
            if (Harvest is null || Harvest.State == HarvestState.Growing) return false;

            harvestResult.IsNormal = Harvest.State == HarvestState.Ready;
            harvestResult.Value = harvestResult.IsNormal 
                ? Harvest.Value 
                : SlopsValue;
            harvestResult.PlantConfig = Harvest.PlantConfig;

            if (harvestResult.IsNormal && harvestResult.PlantConfig.Plant.CanHaveSeed)
            {
                harvestResult.HasSeedInHarvest = CalculateSeedInHarvest();
            }
            
            return true;
        }

        private bool CalculateSeedInHarvest()
        {
            int value = UnityEngine.Random.Range(0, 101);
            return value <= SeedInHarvestProbability;
        }
    }
}