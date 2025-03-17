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

        public Timer Timer { get; }
        public int CurrentDayOfWeek { get; private set; }
        public DayState CurrentDayState { get; private set; }
        public int CurrentWeek { get; private set; }

        public bool StartState => CurrentWeek == 0 && CurrentDayOfWeek == 0;
        
        public void AddListener(ITimeListener listener)
        {
            _listeners.Add(listener);
        }

        public TimeGameCycle(GameSettings settings)
        {
            CurrentDayOfWeek = 0;
            Timer = new Timer(settings.TimeSettings.SecondsIn12Hours, true);
        }

        public void RemoveListener(ITimeListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Initialize(IEnumerable<ITimeListener> listeners)
        {
            AddListeners(listeners);
        }
        
        public void SetCurrentWeek(int currentWeek)
        {
            CurrentWeek = currentWeek;
            OnNewWeekStarted();
        }

        public void SetCurrentDayOfWeek(int currentDayOfWeek)
        {
            CurrentDayOfWeek = currentDayOfWeek;
            foreach (ITimeListener listener in _listeners)
            {
                if (listener is IDayBeginListener initGameListener)
                {
                    initGameListener.SetDay(CurrentDayOfWeek);
                }
            }
        }

        public void SetCurrentDayState(DayState dayState)
        {
            CurrentDayState = dayState;
            if (CurrentDayState == DayState.Night)
            {
                OnNightStarted();
            }
        }

        void IPauseGameListener.OnPause() => Timer.Pause();

        void IResumeGameListener.OnResume() => Timer.Resume();

        void IPrepareGameListener.OnPrepare()
        {
            Timer.Stop();
            CurrentDayOfWeek = 0;
            CurrentWeek = 0;
            CurrentDayState = DayState.Day;
        }

        void IStartGameListener.OnStart()
        {
            Timer.Start();
            Timer.OnEnded += OnTimerEnded;
            OnDayStarted();
        }

        void IFinishGameListener.OnFinish()
        {
            Timer.Stop();
            Timer.OnEnded -= OnTimerEnded;
        }

        void ITickable.Tick() => Timer.Tick(Time.deltaTime);

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