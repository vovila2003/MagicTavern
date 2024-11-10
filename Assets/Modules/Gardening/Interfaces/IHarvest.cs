using System;

namespace Modules.Gardening
{
    internal interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<CaringType, CaringState> OnCaringStateChanged;

        int Value { get; }
        PlantType PlantType { get; }
        bool IsReady { get; }
        CaringType? LostReason { get; }

        void StartGrow();
        void StopGrow();
        void Care(CaringType caringType);
        
        void Tick(float deltaTime);
    }
}