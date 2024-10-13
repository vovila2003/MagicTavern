using System;
using System.Collections.Generic;
using Gardering.Interfaces;
using Timers.Implementations;

namespace Gardering.Implementations
{
    [Serializable]
    public class Harvest : IHarvest
    {
        public event Action<HarvestState> OnStateChanged;
        public event Action<float> OnProgressChanged;
        
        public bool isReady { get; private set; }
        private float _progress;
        private float _value;
        private Timer _growingTimer;
        private List<HarvestAttribute> _attributes = new();
        
        public Harvest(SeedConfig seed)
        {
            SetupAttributes(seed);
            SetupGrowingTimer(seed);
            _value = seed.HarvestValue;
        }

        public void StartGrowiwng()
        {
            throw new NotImplementedException();
        }

        public void StopGrowiwng()
        {
            foreach (HarvestAttribute attribute in _attributes)
            {
                attribute.Dispose();
            }
        }

        private void SetupAttributes(SeedConfig seed)
        {
            CreateHarvestAttribute(AttributeType.Water, seed.Watering);
            CreateHarvestAttribute(AttributeType.Fertilizer, seed.Fertilization);
            CreateHarvestAttribute(AttributeType.Disinfection, seed.Disinfection);
            CreateHarvestAttribute(AttributeType.Heal, seed.Healing);
            CreateHarvestAttribute(AttributeType.Weed, seed.Weeding);
        }

        private void SetupGrowingTimer(SeedConfig seed)
        {
            
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
            
            //подписаться
            
            _attributes.Add(harvestAttribute);
        }
    }

    public class HarvestAttribute
    {
        public event Action<AttributeState> OnStateChanged;
        public event Action OnFail;
        public AttributeType AttributeType { get; private set; }
        public AttributeState State { get; private set; }

        private readonly Timer _timer = new();
        private readonly Timer _criticalTimer = new();

        public HarvestAttribute(AttributeType attributeType, float timerDuration, float criticalTimerDuration)
        {
            AttributeType = attributeType;
            State = AttributeState.Norm;
            SetupTimer(_timer, timerDuration);
            SetupTimer(_criticalTimer, criticalTimerDuration);
            _timer.OnStateChanged += OnTimerStateChanged;
            _criticalTimer.OnEnded += OnFail;
        }

        public void Dispose()
        {
            _timer.Stop();
            _criticalTimer.Stop();
            _timer.OnStateChanged -= OnTimerStateChanged;
            _criticalTimer.OnEnded -= OnFail;
        }

        private void SetupTimer(Timer timer, float timerDuration)
        {
            timer.Duration = timerDuration;
            timer.Loop = true;
            timer.Start();
        }

        private void OnTimerStateChanged(State timerState)
        {
            
        }
    }
}