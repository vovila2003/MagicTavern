using System;
using Modules.Gardening.Enums;
using Modules.Timers.Implementations;

namespace Modules.Gardening
{
    public class HarvestCaring
    {
        public event Action<CaringType, CaringState> OnStateChanged;
        public event Action<CaringType> OnLost;

        private readonly Timer _timer = new();
        private readonly Timer _criticalTimer = new();
        private readonly CaringType _caringType;
        private bool _isDisposed;

        private CaringState _state;

        public HarvestCaring(CaringType caringType, float timerDuration, float criticalTimerDuration)
        {
            _caringType = caringType;
            _isDisposed = false;
            _state = CaringState.Norm;

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
            _state = CaringState.Norm;
            OnStateChanged?.Invoke(_caringType, _state);
            
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
            _state = CaringState.Need;
            OnStateChanged?.Invoke(_caringType, _state);
        }

        private void OnCriticalTimerFail()
        {
            OnLost?.Invoke(_caringType);
        }
    }
}