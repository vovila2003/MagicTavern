using System;

namespace Modules.Gardening
{
    public interface ISeedbed
    {
        event Action<SeedbedState> OnStateChanged;
        event Action<HarvestState> OnHarvestStateChanged;
        event Action<CaringType, CaringState> OnCaringChanged;
        
        CaringType? LostReason { get; }
        SeedbedState State { get; }
        PlantType? PlantType { get; }

        bool Prepare();
        bool Seed(SeedConfig seed);
        bool Gather(out HarvestResult harvestResult);
        void Care(CaringType caringType);
        
        void Tick(float deltaTime);
        void Pause();
        void Resume();
        void Stop();
    }
}