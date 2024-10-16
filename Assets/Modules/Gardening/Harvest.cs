using System;
using System.Collections.Generic;
using Modules.Gardening.Enums;
using Modules.Gardening.Interfaces;
using Modules.Products.Plants;
using Modules.Timers.Implementations;

namespace Modules.Gardening
{
    public class Harvest : IHarvest
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<AttributeType, AttributeState> OnAttributeChanged;
        
        public float Value { get; private set; }
        public PlantType PlantType { get; }
        public bool IsReady => _state == HarvestState.Ready;
        
        private readonly float _readyValue;
        private HarvestState _state;
        private readonly Timer _growthTimer = new();
        private readonly Dictionary<AttributeType, HarvestAttribute> _attributes = new();
        
        public Harvest(SeedConfig seed)
        {
            SetupGrowthTimer(seed);
            SetupAttributes(seed);
            _readyValue = seed.HarvestValue;
            Value = 0;
            _state = HarvestState.NorReady;
            PlantType = seed.Type;
        }

        public void StartGrow()
        {
            _growthTimer.Start();
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Start();
            }
        }

        public void StopGrow()
        {
            _growthTimer.Stop();
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Stop();
            }
            
            Dispose();
        }

        public void Care(AttributeType attributeType)
        {
            if (IsReady) return;
            
            if (!_attributes.TryGetValue(attributeType, out HarvestAttribute attribute)) return;
            
            attribute.Care();            
        }

        
        //TODO delete -> DI
        public void Tick(float deltaTime)
        {
            _growthTimer.Tick(deltaTime);
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Tick(deltaTime);
            }
        }

        private void SetupGrowthTimer(SeedConfig seed)
        {
            _growthTimer.Loop = false;
            _growthTimer.Duration = seed.GrowthDurationInSeconds;
            _growthTimer.OnEnded += OnGrowthEnded;
        }

        private void OnGrowthEnded()
        {
            Value = _readyValue;
            _state = HarvestState.Ready;
            OnStateChanged?.Invoke(HarvestState.Ready);
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Stop();
            }
        }

        private void SetupAttributes(SeedConfig seed)
        {
            foreach (AttributeSettings attributeSettings in seed.Attributes)
            {
                CreateHarvestAttribute(attributeSettings);
            }
        }

        private void CreateHarvestAttribute(AttributeSettings attributeSettings)
        {
            var harvestAttribute = new HarvestAttribute(
                attributeSettings.Type,
                attributeSettings.TimerDurationInSeconds,
                attributeSettings.CriticalTimerDurationInSeconds);

            harvestAttribute.OnLost += OnLost;
            harvestAttribute.OnStateChanged += OnHarvestAttributeChanged;
            
            _attributes.Add(attributeSettings.Type, harvestAttribute);
        }

        private void OnHarvestAttributeChanged(AttributeType type, AttributeState state)
        {
            OnAttributeChanged?.Invoke(type, state);
        }

        private void OnLost(AttributeType attributeType)
        {
            Value = 0;
            _state = HarvestState.Lost;
            OnStateChanged?.Invoke(HarvestState.Lost);
            StopGrow();
        }

        private void Dispose()
        {
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Dispose();
                attribute.OnLost -= OnLost;
                attribute.OnStateChanged -= OnHarvestAttributeChanged;
            }
            _growthTimer.OnEnded -= OnGrowthEnded;
        }
    }
}