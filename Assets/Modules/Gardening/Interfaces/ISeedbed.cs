using System;
using Modules.Gardening.Enums;

namespace Modules.Gardening.Interfaces
{
    public interface ISeedbed
    {
        event Action<SeedbedState> OnStateChanged;
        event Action<HarvestState> OnHarvestStateChanged;
        event Action<AttributeType> OnCareNeeded;
        
        bool Prepare();
        bool Seed(SeedConfig seed);
        bool Gather(out HarvestResult harvestResult);
        void Care(AttributeType attributeType);
        
        //TODO delete -> DI
        void Tick(float deltaTime);
    }
}