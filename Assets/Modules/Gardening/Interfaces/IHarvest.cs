using System;
using Modules.Gardening.Enums;
using Modules.Products.Plants;

namespace Modules.Gardening.Interfaces
{
    internal interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<CaringType, CaringState> OnCaringStateChanged;

        float Value { get; }
        PlantType PlantType { get; }
        bool IsReady { get; }
        CaringType? LostReason { get; }

        void StartGrow();
        void StopGrow();
        void Care(CaringType caringType);
        
        void Tick(float deltaTime);
    }
}