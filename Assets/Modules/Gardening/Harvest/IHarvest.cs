using System;
using Modules.Timers;

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
        bool IsWaterRequired { get; }
        bool IsPaused { get; }
        bool IsReadyAfterWatering { get; }
        bool IsPenalized { get; }
        int ResultHarvestAmount { get; }
        Timer GrowthTimer { get; }
        HarvestWatering HarvestWatering { get; }
        HarvestSickness HarvestSickness { get; }

        void StartGrow();
        void StopGrow();
        void Watering();
        void Heal();
        void BoostHarvestAmount(int boostInPercent);
        void ReduceHarvestSicknessProbability(int reducing);
        void AccelerateGrowth(int accelerationInPercent);

        void Tick(float deltaTime);
    }
}