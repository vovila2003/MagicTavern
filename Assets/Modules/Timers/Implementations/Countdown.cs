using System;
using Sirenix.OdinInspector;
using Timers.Interfaces;
using UnityEngine;

namespace Timers.Implementations
{
    [Serializable]
    public class Countdown : 
        IStartable,
        IStoppable,
        IPausable,
        IResumable,
        IEndable,
        IProgressable,
        ITickable
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

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        private float _duration;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif

        private float _currentTime;
        private State _currentState;

        public Countdown()
        {
        }

        public Countdown(float duration, bool loop = false)
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
        [Title("Methods")]
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

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool Start()
        {
            if (_currentState is not (State.Idle or State.Ended))
            {
                return false;
            }

            _currentTime = _duration;
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
        public void Tick(float deltaTime)
        {
            if (_currentState != State.Playing)
            {
                return;
            }

            _currentTime = Mathf.Max(0, _currentTime - deltaTime);
            OnCurrentTimeChanged?.Invoke(_currentTime);

            float progress = 1 - _currentTime / _duration;
            OnProgressChanged?.Invoke(progress);

            if (progress >= 1)
            {
                Complete();
            }
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

        public float GetProgress()
        {
            return _currentState switch
            {
                State.Playing or State.Paused => 1 - _currentTime / _duration,
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
            float remainingTime = _duration * (1 - progress);

            _currentTime = remainingTime;
            OnCurrentTimeChanged?.Invoke(remainingTime);
            OnProgressChanged?.Invoke(progress);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetDuration(float duration)
        {
            if (duration < 0)
            {
                throw new Exception($"Duration can't be negative: {duration}!");
            }

            if (Math.Abs(_duration - duration) <= float.Epsilon) return;
            
            _duration = duration;
            OnDurationChanged?.Invoke(duration);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetCurrentTime(float time)
        {
            if (time < 0)
            {
                throw new Exception($"Time can't be negative: {_duration}!");
            }

            float newTime = Mathf.Clamp(time, 0, _duration);
            if (Math.Abs(newTime - _currentTime) <= float.Epsilon) return;
            
            _currentTime = newTime;
            OnCurrentTimeChanged?.Invoke(newTime);
            OnProgressChanged?.Invoke(GetProgress());
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ResetTime() => SetCurrentTime(_duration);
    }
}