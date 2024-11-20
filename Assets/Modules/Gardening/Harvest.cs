using System;
using System.Collections.Generic;
using Modules.Timers;
using UnityEngine;

namespace Modules.Gardening
{
    internal class Harvest : IHarvest  
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<Caring, CaringState> OnCaringStateChanged;
        public event Action<int> OnAgeChanged;
        
        public int Value { get; private set; }
        public bool IsReady => _state == HarvestState.Ready;
        public Caring LostReason { get; private set; }
        public PlantConfig PlantConfig { get; }
        public int CurrentAge { get; private set; }

        private readonly int _readyValue;
        private readonly int _maxAge;
        private HarvestState _state;
        private readonly Timer _growthTimer = new();
        private readonly Dictionary<Caring, HarvestCaring> _attributes = new();
        private bool _isEnable; 
        
        public Harvest(PlantConfig plantConfig)
        {
            _readyValue = plantConfig.Plant.ResultValue;
            Value = 0;
            _state = HarvestState.NotReady;
            _maxAge = plantConfig.PlantMetadata.Healthy.Length;
            CurrentAge = -1;
            PlantConfig = plantConfig;
            LostReason = null;
            
            SetupGrowthTimer(plantConfig.Plant);
            SetupCaring(plantConfig.Plant.PlantCaring);
        }

        public void StartGrow()
        {
            OnStateChanged?.Invoke(HarvestState.NotReady);
            _growthTimer.Start();
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Start();
            }
            
            _isEnable = true;
        }

        public void StopGrow()
        {
            _growthTimer.Stop();
            foreach (HarvestCaring attribute in _attributes.Values)
            {
                attribute.Stop();
            }
            
            Dispose();
            _isEnable = false;
        }

        public void Care(Caring caring)
        {
            if (IsReady) return;
            
            if (!_attributes.TryGetValue(caring, out HarvestCaring attribute)) return;
            
            attribute.Care();            
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnable) return;
            
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
            _growthTimer.OnProgressChanged += OnProgressChanged;
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

        private void OnProgressChanged(float progress)
        {
            var currentAge = (int) (progress * _maxAge);
            if (currentAge == CurrentAge) return;
            
            CurrentAge = Mathf.Clamp(currentAge, 0, _maxAge - 1);
            OnAgeChanged?.Invoke(CurrentAge);
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
            _growthTimer.OnProgressChanged -= OnProgressChanged;
        }
    }
}