using System;
using Modules.Gardening.Enums;
using Modules.Timers.Implementations;

namespace Modules.Gardening
{
    public class HarvestAttribute
    {
        public event Action<AttributeType, AttributeState> OnStateChanged;
        public event Action<AttributeType> OnLost;

        private readonly Timer _timer = new();
        private readonly Timer _criticalTimer = new();
        private readonly AttributeType _attributeType;
        private bool _isDisposed;

        private AttributeState _state;

        public HarvestAttribute(AttributeType attributeType, float timerDuration, float criticalTimerDuration)
        {
            _attributeType = attributeType;
            _isDisposed = false;
            _state = AttributeState.Norm;

            _timer.Duration = timerDuration;
            _timer.Loop = true;
            _timer.OnEnded += OnTimerEnded;

            _criticalTimer.Duration = criticalTimerDuration;
            _criticalTimer.Loop = false;
            _criticalTimer.OnEnded += OnCriticalTimerFail;
        }

        public void Start()
        {
            _timer.Start();
            _criticalTimer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _criticalTimer.Stop();
        }

        public void Care()
        {
            _state = AttributeState.Norm;
            OnStateChanged?.Invoke(_attributeType, _state);
            
            _timer.ForceStart();
            _criticalTimer.ForceStart();
        }
        
        //TODO delete -> DI
        public void Tick(float deltaTime)
        {
            _timer.Tick(deltaTime);
            _criticalTimer.Tick(deltaTime);
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            
            _timer.OnEnded -= OnTimerEnded;
            _criticalTimer.OnEnded -= OnCriticalTimerFail;
            _isDisposed = true;
        }

        private void OnTimerEnded()
        {
            _state = AttributeState.Need;
            OnStateChanged?.Invoke(_attributeType, _state);
        }

        private void OnCriticalTimerFail()
        {
            OnLost?.Invoke(_attributeType);
        }
    }
}