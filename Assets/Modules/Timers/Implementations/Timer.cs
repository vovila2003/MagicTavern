using System;
using Sirenix.OdinInspector;
using Timers.Interfaces;
using UnityEngine;

namespace Timers.Implementations
{
    [Serializable]
    public class Timer : ITimer
    {
        
        public event Action OnStarted;
        public event Action OnStopped;
        public event Action OnPaused;
        public event Action OnResumed;
        public event Action OnEnded;
        public event Action<float> OnProgressChanged;
        public event Action<State> OnStateChanged;
        public event Action<float> OnCurrentTimeChanged;
        public event Action<float> OnDurationChanged;
        
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public State CurrentState => _currentState;

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Duration
        {
            get { return _duration; }
            set { SetDuration(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float CurrentTime
        {
            get { return _currentTime; }
            set { SetCurrentTime(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float Progress
        {
            get { return GetProgress(); }
            set { SetProgress(value); }
        }

#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public bool Loop { get; set; }

        private float _duration;
        private float _currentTime;
        private State _currentState;

        public Timer()
        {
        }

        public Timer(float duration, bool loop = false)
        {
                _duration = duration;
                Loop = loop;
        }
        
        public State GetCurrentState() => _currentState;
        public bool IsIdle() => _currentState == State.Idle;
        public bool IsPlaying() => _currentState == State.Playing;
        public bool IsPaused() => _currentState == State.Paused;
        public bool IsEnded() => _currentState == State.Ended;

        public float GetDuration() => _duration;
        public float GetCurrentTime() => _currentTime;
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart()
        {
            Stop(); 
            Start();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ForceStart(float currentTime)
        {
            Stop();
            Start(currentTime);
        }


        public bool Start()
        {
            if (_currentState is not (State.Idle or State.Ended))
            {
                return false;
            }

            _currentState = State.Playing;
            OnStateChanged?.Invoke(State.Playing);
            OnStarted?.Invoke();
            return true;
        }
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start(float currentTime)
        {
            if (_currentState is not (State.Idle or State.Ended))
            {
                return false;
            }

            _currentTime = Mathf.Clamp(currentTime, 0, _duration);
            _currentState = State.Playing;
            OnStateChanged?.Invoke(State.Playing);
            OnStarted?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Play()
        {
            if (_currentState is not (State.Idle or State.Ended))
            {
                return false;
            }
            
            _currentState = State.Playing;
            OnStateChanged?.Invoke(State.Playing);
            OnStarted?.Invoke();
            return true; 
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Stop()
        {
            if (_currentState == State.Idle)
            {
                return false;
            }

            _currentTime = 0;
            _currentState = State.Idle;
            OnStateChanged?.Invoke(State.Idle);
            OnStopped?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Pause()
        {
            if (_currentState != State.Playing)
            {
                return false;
            }

            _currentState = State.Paused;
            OnStateChanged?.Invoke(State.Paused);
            OnPaused?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Resume()
        {
            if (_currentState != State.Paused)
            {
                return false;
            }

            _currentState = State.Playing;
            OnStateChanged?.Invoke(State.Playing);
            OnResumed?.Invoke();
            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Tick(float deltaTime)
        {
            if (_currentState != State.Playing)
            {
                return;
            }

            _currentTime = Mathf.Min(_duration, _currentTime + deltaTime);
            OnCurrentTimeChanged?.Invoke(_currentTime);

            float progress = _currentTime / _duration;
            OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
            {
                Complete();
            }
        }

        public float GetProgress()
        {
            return _currentState switch
            {
                State.Playing or State.Paused => _currentTime / _duration,
                State.Ended => 1,
                _ => 0
            };
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            _currentTime = _duration * progress;
            OnCurrentTimeChanged?.Invoke(_currentTime);
            OnProgressChanged?.Invoke(progress);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        private void SetDuration(float duration)
        {
            if (duration < 0)
            {
                return;
            }

            if (Math.Abs(_duration - duration) <= float.Epsilon) return;
            
            _duration = duration;
            OnDurationChanged?.Invoke(duration);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        private void SetCurrentTime(float time)
        {
            if (time < 0)
            {
                return;
            }

            float newTime = Mathf.Clamp(time, 0, _duration);
            if (Math.Abs(newTime - _currentTime) <= float.Epsilon) return;
            
            _currentTime = newTime;
            OnCurrentTimeChanged?.Invoke(newTime);
            OnProgressChanged?.Invoke(GetProgress());
        }

        private void Complete()
        {
            _currentState = State.Ended;
            OnStateChanged?.Invoke(State.Ended);
            OnEnded?.Invoke();

            if (Loop)
            {
                Start();
            }
        }
    }
}