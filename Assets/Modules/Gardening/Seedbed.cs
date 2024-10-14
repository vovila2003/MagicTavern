using System;
using Sirenix.OdinInspector;

namespace Gardening
{
    [Serializable]
    public class Seedbed : ISeedbed
    {
        public event Action<SeedbedState> OnStateChanged;
        public event Action<HarvestState> OnHarvestStateChanged;
        public event Action<AttributeType> OnCareNeeded;
        
        public bool IsSeeded => _state == SeedbedState.Seeded;
            
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private SeedbedState _state = SeedbedState.NotReady;
        private IHarvest _harvest;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Prepare()
        {
            if (_state is not SeedbedState.NotReady) return false;
            
            _state = SeedbedState.Ready;
            OnStateChanged?.Invoke(SeedbedState.Ready);
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Seed(SeedConfig seed) 
        {
            if (_state != SeedbedState.Ready) return false;
            
            _harvest = new Harvest(seed);
            StartGrow();
            _state = SeedbedState.Seeded;
            OnStateChanged?.Invoke(SeedbedState.Seeded);
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Gather(out HarvestResult harvestResult)
        {
            harvestResult = new HarvestResult{Type = _harvest.PlantType};
            
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

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Care(AttributeType attributeType)
        {
            _harvest.Care(attributeType);
        }

        private void StartGrow()
        {
            _harvest.StartGrow();
            _harvest.OnStateChanged += OnHarvestStateChanged;
            _harvest.OnAttributeChanged += OnHarvestAttributeChanged;
        }

        private void StopGrow()
        {
            _harvest.StopGrow();
            _harvest.OnStateChanged -= OnHarvestStateChanged;
            _harvest.OnAttributeChanged -= OnHarvestAttributeChanged;
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