using System;

namespace Modules.Gardening
{
    internal interface IHarvest
    {
        event Action<HarvestState> OnStateChanged;
        event Action<Caring, CaringState> OnCaringStateChanged;

        int Value { get; }
        Plant Plant { get; }
        bool IsReady { get; }
        Caring LostReason { get; }

        void StartGrow();
        void StopGrow();
        void Care(Caring caring);
        
        void Tick(float deltaTime);
    }
}