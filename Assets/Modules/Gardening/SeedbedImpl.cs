using System;

namespace Modules.Gardening
{
    public class SeedbedImpl : ISeedbed
    {
        public event Action<SeedbedState> OnStateChanged;

        public event Action<HarvestState> OnHarvestStateChanged;

        public event Action<Caring, CaringState> OnCaringChanged;

        public Caring LostReason => _harvest?.LostReason;
        public SeedbedState State => _state;

        private SeedbedState _state = SeedbedState.NotReady;
        private IHarvest _harvest;
        private bool _isEnable;

        public bool Prepare()
        {
            if (_state is not SeedbedState.NotReady) return false;
            
            _state = SeedbedState.Ready;
            OnStateChanged?.Invoke(SeedbedState.Ready);
            return true;
        }

        public bool Seed(PlantConfig plant) 
        {
            if (_state != SeedbedState.Ready) return false;

            _harvest = new Harvest(plant);
            StartGrow();
            _state = SeedbedState.Seeded;
            OnStateChanged?.Invoke(SeedbedState.Seeded);
            return true;
        }

        public bool Gather(out HarvestResult harvestResult) 
        {
            harvestResult = new HarvestResult();
            if (_harvest is null)
            {
                return false;
            }

            if (_state != SeedbedState.Seeded ||
                !_harvest.IsReady)
            {
                harvestResult.IsCollected = false;
                harvestResult.Value = 0;
                return false;
            }

            harvestResult.Value = _harvest.Value;
            harvestResult.IsCollected = true;
            harvestResult.Plant = _harvest.Plant;

            StopGrow();
            
            return true;
        }

        public void Care(Caring caringType)
        {
            _harvest?.Care(caringType);
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnable) return;
            
            _harvest?.Tick(deltaTime);
        }

        public void Pause()
        {
            _isEnable = false;
        }

        public void Resume()
        {
            _isEnable = true;
        }

        public void Stop()
        {
            StopGrow();
        }

        private void StartGrow()
        {
            _isEnable = true;
            if (_harvest is null) return;
            
            _harvest.StartGrow();
            _harvest.OnStateChanged += OnHarvestStateChangedImpl;
            _harvest.OnCaringStateChanged += HarvestCaringStateChanged;
        }

        private void StopGrow()
        {
            _isEnable = false;
            _state = SeedbedState.NotReady;
            OnStateChanged?.Invoke(SeedbedState.NotReady);
            
            if (_harvest is null) return;

            _harvest.StopGrow();
            _harvest.OnStateChanged -= OnHarvestStateChangedImpl;
            _harvest.OnCaringStateChanged -= HarvestCaringStateChanged;
            _harvest = null;
        }


        private void OnHarvestStateChangedImpl(HarvestState state)
        {
            OnHarvestStateChanged?.Invoke(state);
            if (state == HarvestState.Lost)
            {
                _state = SeedbedState.NotReady;
            }
        }

        private void HarvestCaringStateChanged(Caring type, CaringState state)
        {
            OnCaringChanged?.Invoke(type, state);
        }
    }
}