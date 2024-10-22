using System;
using System.Collections.Generic;
using Modules.Timers;

namespace Modules.Gardening
{
    internal class Harvest : IHarvest
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<CaringType, CaringState> OnCaringStateChanged;
        
        public int Value { get; private set; }
        public PlantType PlantType { get; }
        public bool IsReady => _state == HarvestState.Ready;
        public CaringType? LostReason { get; private set; }
        
        private readonly int _readyValue;
        private HarvestState _state;
        private readonly Timer _growthTimer = new();
        private readonly Dictionary<CaringType, HarvestCaring> _attributes = new();
        
        public Harvest(SeedConfig seed)
        {
            SetupGrowthTimer(seed);
            SetupCarings(seed);
            _readyValue = seed.Value;
            Value = 0;
            _state = HarvestState.NorReady;
            PlantType = seed.Type;
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

        public void Care(CaringType caringType)
        {
            if (IsReady) return;
            
            if (!_attributes.TryGetValue(caringType, out HarvestCaring attribute)) return;
            
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

        private void SetupGrowthTimer(SeedConfig seed)
        {
            _growthTimer.Loop = false;
            _growthTimer.Duration = seed.GrowthDuration;
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

        private void SetupCarings(SeedConfig seed)
        {
            foreach (CaringSettings attributeSettings in seed.PlantCaring)
            {
                CreateHarvestCaring(attributeSettings);
            }
        }

        private void CreateHarvestCaring(CaringSettings caringSettings)
        {
            var harvestCaring = new HarvestCaring(
                caringSettings.CaringType,
                caringSettings.Duration,
                caringSettings.IsCriticalEnabled,
                caringSettings.CriticalDuration);

            harvestCaring.OnLost += OnLost;
            harvestCaring.OnStateChanged += OnHarvestCaringChanged;
            
            _attributes.Add(caringSettings.CaringType, harvestCaring);
        }

        private void OnHarvestCaringChanged(CaringType type, CaringState state)
        {
            OnCaringStateChanged?.Invoke(type, state);
        }

        private void OnLost(CaringType caringType)
        {
            Value = 0;
            _state = HarvestState.Lost;
            LostReason = caringType;
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