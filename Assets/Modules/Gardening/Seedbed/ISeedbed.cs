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
        SeedbedBoost SeedbedBoost { get; }

        bool Seed(PlantConfig seed);
        bool Gather(out HarvestResult harvestResult);
        void Watering();
        void Heal();
        void ReduceHarvestSicknessProbability(int reducing);

        void Tick(float deltaTime);
        void Pause();
        void Resume();
        void Stop();
    }
}