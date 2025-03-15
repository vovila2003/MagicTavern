using System;

namespace Modules.Gardening
{
    public interface ISeedbed
    {
        event Action<HarvestState> OnHarvestStateChanged;
        event Action<HarvestAge> OnHarvestAgeChanged;
        event Action<bool> OnHarvestWateringRequired;
        event Action<bool> OnHealingRequired;
        event Action<float> OnHarvestProgressChanged;
        event Action OnGathered;
        event Action<float> OnDryingTimerProgressChanged;
        
        IHarvest Harvest { get; }
        bool IsFertilized { get; }
        bool IsBoosted { get; set; }
        bool IsSickReduced { get; set; }
        bool IsAccelerated { get; set; }
        int SeedInHarvestProbability { get; set; }

        bool Seed(PlantConfig seed);
        bool Gather(out HarvestResult harvestResult);
        void Watering();
        void Heal();
        bool ReduceHarvestSicknessProbability(int reducing);
        bool BoostHarvestAmount(int boostInPercent);
        bool AccelerateGrowth(int accelerationInPercent);
        void SetSeedInHarvestProbability(int probability);

        void Tick(float deltaTime);
        void Pause();
        void Resume();
        void Stop();
    }
}