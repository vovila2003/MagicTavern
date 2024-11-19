using System;
using System.Collections.Generic;
using Modules.Timers;

namespace Modules.Gardening
{
    internal class Harvest : IHarvest  
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<Caring, CaringState> OnCaringStateChanged;
        
        public int Value { get; private set; }
        public bool IsReady => _state == HarvestState.Ready;
        public Caring LostReason { get; private set; }
        public Plant Plant { get; }

        private readonly int _readyValue;
        private HarvestState _state;
        private readonly Timer _growthTimer = new();
        private readonly Dictionary<Caring, HarvestCaring> _attributes = new();
        
        public Harvest(PlantConfig plantConfig)
        {
            SetupGrowthTimer(plantConfig.Plant);
            SetupCaring(plantConfig.Plant.PlantCaring);
            _readyValue = plantConfig.Plant.ResultValue;
            Value = 0;
            _state = HarvestState.NotReady;
            Plant = plantConfig.Plant;
            LostReason = null;
        }

        public void StartGrow()
        {
            _growthTimer.Start();
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Start();
            }
        }

        public void StopGrow()
        {
            _growthTimer.Stop();
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Stop();
            }
            
            Dispose();
        }

        public void Care(Caring caring)
        {
            if (IsReady) return;
            
            if (!_attributes.TryGetValue(caring, out HarvestCaring attribute)) return;
            
            attribute.Care();            
        }

        public void Tick(float deltaTime)
        {
            _growthTimer.Tick(deltaTime);
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Tick(deltaTime);
            }
        }

        private void SetupGrowthTimer(Plant plant)
        {
            _growthTimer.Loop = false;
            _growthTimer.Duration = plant.GrowthDuration;
            _growthTimer.OnEnded += OnGrowthEnded;
        }

        private void OnGrowthEnded()
        {
            Value = _readyValue;
            _state = HarvestState.Ready;
            OnStateChanged?.Invoke(HarvestState.Ready);
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Stop();
            }
        }

        private void SetupCaring(IEnumerable<CaringConfig> caringList)
        {
            foreach (CaringConfig caringConfig in caringList)
            {
                CreateHarvestCaring(caringConfig);
            }
        }

        private void CreateHarvestCaring(CaringConfig caringConfig)
        {
            var harvestCaring = new HarvestCaring(caringConfig);

            harvestCaring.OnLost += OnLost;
            harvestCaring.OnStateChanged += OnHarvestCaringChanged;
            
            _attributes.Add(caringConfig.Caring, harvestCaring);
        }

        private void OnHarvestCaringChanged(Caring type, CaringState state)
        {
            OnCaringStateChanged?.Invoke(type, state);
        }

        private void OnLost(Caring caring)
        {
            Value = 0;
            _state = HarvestState.Lost;
            LostReason = caring;
            OnStateChanged?.Invoke(HarvestState.Lost);
            StopGrow();
        }

        private void Dispose()
        {
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Dispose();
                attribute.OnLost -= OnLost;
                attribute.OnStateChanged -= OnHarvestCaringChanged;
            }
            _growthTimer.OnEnded -= OnGrowthEnded;
        }
    }
}