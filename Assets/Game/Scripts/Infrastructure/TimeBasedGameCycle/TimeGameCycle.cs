using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Timers;
using Tavern.Settings;
using UnityEngine;
using ITickable = VContainer.Unity.ITickable;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class TimeGameCycle : 
        IPrepareGameListener,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        ITickable
    {
        private const int DaysInWeek = 7;
        
        private readonly List<ITimeListener> _listeners = new();

        private readonly Timer _timer;
        public int CurrentDayOfWeek { get; private set; }
        public DayState CurrentDayState { get; private set; }
        public int CurrentWeek { get; private set; }
        
        public void AddListener(ITimeListener listener)
        {
            _listeners.Add(listener);
        }

        public TimeGameCycle(GameSettings settings)
        {
            CurrentDayOfWeek = 0;
            _timer = new Timer(settings.TimeSettings.SecondsIn12Hours, true);
        }

        public void RemoveListener(ITimeListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Initialize(IEnumerable<ITimeListener> listeners)
        {
            AddListeners(listeners);
        }

        void IPauseGameListener.OnPause() => _timer.Pause();

        void IResumeGameListener.OnResume() => _timer.Resume();

        void IPrepareGameListener.OnPrepare()
        {
            _timer.Stop();
            CurrentDayOfWeek = 0;
            CurrentWeek = 0;
            CurrentDayState = DayState.Day;
        }

        void IStartGameListener.OnStart()
        {
            _timer.Start();
            _timer.OnEnded += OnTimerEnded;
            OnDayStarted();
        }

        void IFinishGameListener.OnFinish()
        {
            _timer.Stop();
            _timer.OnEnded -= OnTimerEnded;
        }

        void ITickable.Tick() => _timer.Tick(Time.deltaTime);

        private void AddListeners(IEnumerable<ITimeListener> listeners)
        {
            _listeners.AddRange(listeners);
        }

        private void OnTimerEnded()
        {
            if (CurrentDayState == DayState.Day)
            {
                CurrentDayState = DayState.Night;
                OnNightStarted();
                return;
            }
            
            CurrentDayState = DayState.Day;
            CurrentDayOfWeek++;
            if (CurrentDayOfWeek == DaysInWeek)
            {
                CurrentDayOfWeek = 0;
                CurrentWeek++;
                OnNewWeekStarted();
            }
            
            OnDayStarted();
        }

        private void OnDayStarted()
        {
            foreach (ITimeListener listener in _listeners)
            {
                if (listener is IDayBeginListener initGameListener)
                {
                    initGameListener.OnDayBegin(CurrentDayOfWeek);
                }
            }
        }

        private void OnNightStarted()
        {
            foreach (ITimeListener listener in _listeners)
            {
                if (listener is INightBeginListener initGameListener)
                {
                    initGameListener.OnNightBegin();
                }
            }
        }

        private void OnNewWeekStarted()
        {
            foreach (ITimeListener listener in _listeners)
            {
                if (listener is INewWeekListener initGameListener)
                {
                    initGameListener.OnNewWeek(CurrentWeek);
                }
            }
        }
    }
}