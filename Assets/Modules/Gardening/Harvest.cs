using System;
using System.Collections.Generic;
using Modules.Products;
using Timers.Implementations;

namespace Gardening
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
        private Timer _growingTimer;
        private Dictionary<AttributeType, HarvestAttribute> _attributes = new();
        
        public Harvest(SeedConfig seed)
        {
            SetupGrowingTimer(seed);
            SetupAttributes(seed);
            _readyValue = seed.HarvestValue;
            Value = 0;
            _state = HarvestState.NorReady;
            PlantType = seed.Type;
        }

        public void StartGrow()
        {
            _growingTimer.Start();
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Start();
            }
        }

        public void StopGrow()
        {
            _growingTimer.Stop();
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

        private void SetupGrowingTimer(SeedConfig seed)
        {
            _growingTimer.Loop = false;
            _growingTimer.Duration = seed.GrowingDurationInSeconds;
            _growingTimer.OnEnded += OnGrowingEnded;
        }

        private void OnGrowingEnded()
        {
            Value = _readyValue;
            OnStateChanged?.Invoke(HarvestState.Ready);
        }

        private void SetupAttributes(SeedConfig seed)
        {
            CreateHarvestAttribute(AttributeType.Water, seed.Watering);
            CreateHarvestAttribute(AttributeType.Fertilizer, seed.Fertilization);
            CreateHarvestAttribute(AttributeType.Disinfection, seed.Disinfection);
            CreateHarvestAttribute(AttributeType.Heal, seed.Healing);
            CreateHarvestAttribute(AttributeType.Weed, seed.Weeding);
        }

        private void CreateHarvestAttribute(AttributeType type, AttributeSettings attributeSettings)
        {
            if (!attributeSettings.IsEnabled)
            {
                return;
            }

            var harvestAttribute = new HarvestAttribute(
                type,
                attributeSettings.TimerDurationInSeconds,
                attributeSettings.CriticalTimerDurationInSeconds);

            harvestAttribute.OnFail += OnFail;
            harvestAttribute.OnStateChanged += OnAttributeChanged;
            
            _attributes.Add(type, harvestAttribute);
        }

        private void OnFail(AttributeType attributeType)
        {
            OnStateChanged?.Invoke(HarvestState.Lost);
        }

        private void Dispose()
        {
            foreach (HarvestAttribute attribute in _attributes.Values)
            {
                attribute.Dispose();
                attribute.OnFail -= OnFail;
                attribute.OnStateChanged -= OnAttributeChanged;
            }
            _growingTimer.OnEnded -= OnGrowingEnded;
        }
    }
}