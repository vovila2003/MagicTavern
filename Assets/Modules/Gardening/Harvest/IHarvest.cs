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
        event Action OnSick;

        int Value { get; }
        HarvestState State { get; }
        HarvestAge Age { get; }
        PlantConfig PlantConfig { get; }
        bool IsSick { get; }
        int SickProbability { get; }

        void StartGrow();
        void StopGrow();
        void Watering();
        void Heal(int medicineReducing);
        
        void Tick(float deltaTime);
    }
}