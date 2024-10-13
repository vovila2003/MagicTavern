using System;
using Gardering.Implementations;

namespace Gardering.Interfaces
{
    public interface IHarvest
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<float> OnProgressChanged;
        
        bool isReady { get; }
        void StartGrowiwng();
        void StopGrowiwng();
    }
}