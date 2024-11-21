using System;

namespace Modules.Gardening
{
    public interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<HarvestAge> OnAgeChanged;
        event Action OnWaterRequired;
        event Action<float> OnProgressChanged;
        event Action<float> OnDryingTimerProgressChanged;

        int Value { get; }
        HarvestState State { get; }
        HarvestAge Age { get; }
        PlantConfig PlantConfig { get; }

        void StartGrow();
        void StopGrow();
        void Watering();
        
        void Tick(float deltaTime);
    }
}