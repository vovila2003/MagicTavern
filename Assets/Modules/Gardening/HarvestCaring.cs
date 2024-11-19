using System;
using Modules.Timers;

namespace Modules.Gardening
{
    internal class HarvestCaring
    {
        public event Action<CaringType, CaringState> OnStateChanged;
        public event Action<CaringType> OnLost;

        private readonly Timer _timer;
        private readonly Timer _criticalTimer;
        private readonly CaringType _caringType;
        private readonly bool _isCriticalEnabled;
        private bool _isDisposed;

        private CaringState _state;

        public HarvestCaring(CaringType caringType, float timerDuration, 
            bool isCriticalEnabled,  float criticalTimerDuration)
        {
            _caringType = caringType;
            _isDisposed = false;
            _state = CaringState.Norm;

            _timer = new Timer(timerDuration, loop:true);
            _timer.OnEnded += OnTimerEnded;

            _isCriticalEnabled = isCriticalEnabled;
            if (!_isCriticalEnabled) return;

            _criticalTimer = new Timer(criticalTimerDuration, loop: false);
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
            OnStateChanged?.Invoke(_caringType, _state);
            
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
            OnStateChanged?.Invoke(_caringType, _state);
        }

        private void OnCriticalTimerFail()
        {
            OnLost?.Invoke(_caringType);
        }
    }
}