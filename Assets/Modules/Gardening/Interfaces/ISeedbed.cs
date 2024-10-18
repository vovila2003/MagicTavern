using System;
using Modules.Gardening.Enums;

namespace Modules.Gardening.Interfaces
{
    public interface ISeedbed
    {
        event Action<SeedbedState> OnStateChanged;
        event Action<HarvestState> OnHarvestStateChanged;
        event Action<CaringType, CaringState> OnCaringChanged;
        
        CaringType? LostReason { get; }
        
        bool Prepare();
        bool Seed(SeedConfig seed);
        bool Gather(out HarvestResult harvestResult);
        void Care(CaringType caringType);
        
        void Tick(float deltaTime);
        void Pause();
        void Resume();
    }
}