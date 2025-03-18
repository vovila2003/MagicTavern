using System;
using Modules.Timers;

namespace Modules.Gardening
{
    public interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<HarvestAge, bool> OnAgeChanged;
        event Action OnWaterRequired;
        event Action<float> OnProgressChanged;
        event Action<float> OnDryingTimerProgressChanged;

        int Value { get; set; }
        HarvestState State { get; }
        HarvestAge Age { get; }
        PlantConfig PlantConfig { get; }
        bool IsSick { get;set; }
        int SickProbability { get; }
        bool IsWaterRequired { get; }
        bool IsPaused { get; set;}
        bool IsReadyAfterWatering { get; set;}
        bool IsPenalized { get; set;}
        int ResultHarvestAmount { get; set;}
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
        void SetIsWaterRequired(bool isWaterRequired);
        void SetState(HarvestState state);
        void SetAge(HarvestAge age);
    }
}