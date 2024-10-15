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
            
            SetupTimer(_timer, timerDuration);
            _timer.OnEnded += OnTimerEnded;
            
            SetupTimer(_criticalTimer, criticalTimerDuration);
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

        public void Dispose()
        {
            if (_isDisposed) return;
            
            _timer.OnEnded -= OnTimerEnded;
            _criticalTimer.OnEnded -= OnCriticalTimerFail;
            _isDisposed = true;
        }

        private static void SetupTimer(Timer timer, float timerDuration)
        {
            timer.Duration = timerDuration;
            timer.Loop = true;
        }

        private void OnTimerEnded()
        {
            _state = AttributeState.Need;
            OnStateChanged?.Invoke(_attributeType, _state);
        }

        private void OnCriticalTimerFail() => OnLost?.Invoke(_attributeType);
    }
}