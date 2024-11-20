using System;

namespace Modules.Gardening
{
    public interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<Caring, CaringState> OnCaringStateChanged;
        event Action<int> OnAgeChanged;

        int Value { get; }
        int CurrentAge { get; }
        PlantConfig PlantConfig { get; }
        bool IsReady { get; }
        Caring LostReason { get; }

        void StartGrow();
        void StopGrow();
        void Care(Caring caring);
        
        void Tick(float deltaTime);
    }
}