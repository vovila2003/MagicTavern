using System;
using Modules.Products;

namespace Gardening
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