using System;
using Modules.Gardening.Enums;

namespace Modules.Gardening.Interfaces
{
    public interface ISeedbed
    {
        event Action<SeedbedState> OnStateChanged;
        event Action<HarvestState> OnHarvestStateChanged;
        event Action<AttributeType> OnCareNeeded;
        
        bool IsSeeded { get; }
        
        bool Prepare();
        bool Seed(SeedConfig seed);
        bool Gather(out HarvestResult harvestResult);
    }
}