using System;
using Modules.Gardening.Enums;
using Modules.Products.Plants;

namespace Modules.Gardening.Interfaces
{
    public interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<AttributeType, AttributeState> OnAttributeChanged;

        float Value { get; }
        PlantType PlantType { get; }
        bool IsReady { get; }
        
        void StartGrow();
        void StopGrow();
        void Care(AttributeType attributeType);
    }
}