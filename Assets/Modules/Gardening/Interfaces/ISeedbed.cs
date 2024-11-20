using System;

namespace Modules.Gardening
{
    public interface ISeedbed
    {
        event Action<SeedbedState> OnStateChanged;
        event Action<HarvestState> OnHarvestStateChanged;
        event Action<Caring, CaringState> OnCaringChanged;
        
        event Action<int> OnHarvestAgeChanged;
        
        Caring LostReason { get; }
        SeedbedState State { get; }
        IHarvest Harvest { get; }

        bool Prepare();
        bool Seed(PlantConfig seed);
        bool Gather(out HarvestResult harvestResult);
        void Care(Caring caringType);
        
        void Tick(float deltaTime);
        void Pause();
        void Resume();
        void Stop();
    }
}