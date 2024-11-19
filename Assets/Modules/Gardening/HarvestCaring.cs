using System;
using Modules.Timers;

namespace Modules.Gardening
{
    internal class HarvestCaring
    {
        public event Action<Caring, CaringState> OnStateChanged;
        public event Action<Caring> OnLost;

        private readonly Timer _timer;
        private readonly Timer _criticalTimer;
        private readonly Caring _caring;
        private readonly bool _isCriticalEnabled;
        private bool _isDisposed;

        private CaringState _state;

        public HarvestCaring(CaringConfig caringConfig)
        {
            _caring = caringConfig.Caring;
            _isDisposed = false;
            _state = CaringState.Norm;

            _timer = new Timer(caringConfig.Duration, loop:true);
            _timer.OnEnded += OnTimerEnded;

            _isCriticalEnabled = caringConfig.IsCriticalEnabled;
            if (!_isCriticalEnabled) return;

            _criticalTimer = new Timer(caringConfig.CriticalDuration, loop: false);
            _criticalTimer.OnEnded += OnCriticalTimerFail;
        }

        public void Start()
        {
            _timer.Start();
            if (_isCriticalEnabled)
            {
                _criticalTimer.Start();
            }
        }

        public void Stop()
        {
            _timer.Stop();
            if (_isCriticalEnabled)
            {
                _criticalTimer.Stop();
            }
        }

        public void Care()
        {
            _state = CaringState.Norm;
            OnStateChanged?.Invoke(_caring, _state);
            
            _timer.ForceStart();
            if (_isCriticalEnabled)
            {
                _criticalTimer.ForceStart();
            }
        }
        
        public void Tick(float deltaTime)
        {
            _timer.Tick(deltaTime);
            if (_isCriticalEnabled)
            {
                _criticalTimer.Tick(deltaTime);
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            
            _timer.OnEnded -= OnTimerEnded;
            if (_isCriticalEnabled)
            {
                _criticalTimer.OnEnded -= OnCriticalTimerFail;
            }

            _isDisposed = true;
        }

        private void OnTimerEnded()
        {
            _state = CaringState.Need;
            OnStateChanged?.Invoke(_caring, _state);
        }

        private void OnCriticalTimerFail()
        {
            OnLost?.Invoke(_caring);
        }
    }
}