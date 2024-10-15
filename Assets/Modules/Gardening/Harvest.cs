using System;
using System.Collections.Generic;
using Modules.Gardening.Enums;
using Modules.Gardening.Interfaces;
using Modules.Products.Plants;
using Modules.Timers.Implementations;

namespace Modules.Gardening
{
    [Serializable]
    public class Harvest : IHarvest
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<AttributeType, AttributeState> OnAttributeChanged;
        
        public float Value { get; private set; }
        public PlantType PlantType { get; private set; }
        public bool IsReady => _state == HarvestState.Ready;
        
        private float _readyValue;
        private HarvestState _state;
        private Timer _growthTimer;
        private Dictionary<AttributeType, HarvestAttribute> _attributes = new();
        
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
            if (!_attributes.TryGetValue(attributeType, out HarvestAttribute attribute)) return;
            
            attribute.Care();            
        }

        private void SetupGrowthTimer(SeedConfig seed)
        {
            _growthTimer.Loop = false;
            _growthTimer.Duration = seed.GrowthDurationInSeconds;
            _growthTimer.OnEnded += GrowthEnded;
        }

        private void GrowthEnded()
        {
            Value = _readyValue;
            OnStateChanged?.Invoke(HarvestState.Ready);
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

            harvestAttribute.OnLost += Lost;
            harvestAttribute.OnStateChanged += OnAttributeChanged;
            
            _attributes.Add(attributeSettings.Type, harvestAttribute);
        }

        private void Lost(AttributeType attributeType)
        {
            OnStateChanged?.Invoke(HarvestState.Lost);
        }

        private void Dispose()
        {
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Dispose();
                attribute.OnLost -= Lost;
                attribute.OnStateChanged -= OnAttributeChanged;
            }
            _growthTimer.OnEnded -= GrowthEnded;
        }
    }
}