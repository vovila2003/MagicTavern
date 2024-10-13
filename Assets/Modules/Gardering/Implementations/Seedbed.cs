using System;
using Gardering.Interfaces;

namespace Gardering.Implementations
{
    [Serializable]
    public class Seedbed : ISeedbed
    {
        public event Action<SeedbedState> OnStateChanged;
        public event Action<HarvestState> OnHarvestStateChanged; 
        public event Action<float> OnHarvestProgressChanged;
        
        public bool IsSeeded => _state == SeedbedState.Seeded;
            
        private SeedbedState _state = SeedbedState.NotReady;
        private IHarvest _harvest;

        public bool Prepare()
        {
            if (_state is not SeedbedState.NotReady) return false;
            
            _state = SeedbedState.Ready;
            OnStateChanged?.Invoke(SeedbedState.Ready);
            return true;
        }

        public bool Seed(SeedConfig seed) 
        {
            if (_state != SeedbedState.Ready) return false;
            
            _harvest = new Harvest(seed);
            StartGrowing();
            _state = SeedbedState.Seeded;
            OnStateChanged?.Invoke(SeedbedState.Seeded);
            return true;
        }

        public bool Gather(out IHarvest harvest)
        {
            if (_state != SeedbedState.Seeded ||
                !_harvest.isReady)
            {
                harvest = null;
                return false;
            }

            StopGrowing();
            harvest = _harvest;
            _harvest = null;
            _state = SeedbedState.NotReady;
            OnStateChanged?.Invoke(SeedbedState.NotReady);
            return true;
        }

        private void StartGrowing()
        {
            _harvest.StartGrowiwng();
            _harvest.OnStateChanged += OnHarvestStateChanged;
            _harvest.OnProgressChanged += OnHarvestProgressChanged;
        }

        private void StopGrowing()
        {
            _harvest.StopGrowiwng();
            _harvest.OnStateChanged -= OnHarvestStateChanged;
            _harvest.OnProgressChanged -= OnHarvestProgressChanged;
        }
    }
}