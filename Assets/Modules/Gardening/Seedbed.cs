using System;
using Modules.Gardening.Enums;
using Modules.Gardening.Interfaces;

namespace Modules.Gardening
{
    [Serializable]
    public class Seedbed : ISeedbed
    {
        public event Action<SeedbedState> OnStateChanged;
        public event Action<HarvestState> OnHarvestStateChanged;
        public event Action<AttributeType> OnCareNeeded;
        
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

            harvestResult.Type = _harvest.PlantType;
            
            if (_state != SeedbedState.Seeded ||
                !_harvest.IsReady)
            {
                harvestResult.IsCorrect = false;
                harvestResult.Value = 0;
                return false;
            }

            StopGrow();
            
            harvestResult.Value = _harvest.Value;
            harvestResult.IsCorrect = true;
            
            _harvest = null; 
            _state = SeedbedState.NotReady;
            OnStateChanged?.Invoke(SeedbedState.NotReady);
            
            return true;
        }

        public void Care(AttributeType attributeType)
        {
            _harvest?.Care(attributeType);
        }

        public void Tick(float deltaTime)
        {
            _harvest?.Tick(deltaTime);
        }

        private void StartGrow()
        {
            if (_harvest is null) return;
            
            _harvest.StartGrow();
            _harvest.OnStateChanged += OnHarvestStateChangedImpl;
            _harvest.OnAttributeChanged += OnHarvestAttributeChanged;
        }

        private void StopGrow()
        {
            if (_harvest is null) return;
            
            _harvest.StopGrow();
            _harvest.OnStateChanged -= OnHarvestStateChangedImpl;
            _harvest.OnAttributeChanged -= OnHarvestAttributeChanged;
        }


        private void OnHarvestStateChangedImpl(HarvestState state)
        {
            OnHarvestStateChanged?.Invoke(state);
            if (state == HarvestState.Lost)
            {
                _state = SeedbedState.NotReady;
            }
        }

        private void OnHarvestAttributeChanged(AttributeType type, AttributeState state)
        {
            if (state == AttributeState.Need)
            {
                OnCareNeeded?.Invoke(type);
            }
        }
    }
}