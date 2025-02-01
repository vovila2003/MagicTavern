using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Timers;
using UnityEngine;
using ITickable = VContainer.Unity.ITickable;
using Random = UnityEngine.Random;

namespace Tavern.Cooking.MiniGame
{
    [UsedImplicitly]
    public class MiniGame : 
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        public static readonly Dictionary<Result, int> Results = new()
        {
            { Result.Red , -2},
            { Result.Yellow , -1},
            { Result.Green , 2},
        };

        public event Action<float> OnValueChanged;
        public event Action OnTimeUp;
        public event Action<float> OnTimeChanged;
        
        private float _value;
        private int _factor;
        private float _speed;
        private Regions _regions;
        private readonly Countdown _timer = new();

        public bool IsPlaying { get; private set; }

        public Regions CreateGame(MiniGameConfig config, int time)
        {
            _speed = Random.Range(config.MinSpeedValue, config.MaxSpeedValue);
            _timer.Duration = time;
            return SetZones(config);
        } 

        public void StartGame()
        {
            StartTimer();

            IsPlaying = true;
            _factor = 1;
            _value = 0;
        }

        public int StopGame()
        {
            StopTimer();

            IsPlaying = false;
            return GetResult();
        }

        void ITickable.Tick()
        {
            if (!IsPlaying) return;

            float deltaTime = Time.deltaTime;
            _value += _factor * deltaTime * _speed;
            _factor = _value switch
            {
                > 1 => -1,
                < 0 => 1,
                _ => _factor
            };
            _value = Mathf.Clamp01(_value);
            _timer.Tick(deltaTime);
            
            OnValueChanged?.Invoke(_value);
        }

        private void OnCurrentTimeChanged(float value) => OnTimeChanged?.Invoke(value);

        private void OnTimerEnded() => OnTimeUp?.Invoke();

        void IPauseGameListener.OnPause()
        {
            IsPlaying = false;
            _timer.Pause();
        }

        void IResumeGameListener.OnResume()
        {
            IsPlaying = true;
            _timer.Resume();
        }

        private Regions SetZones(MiniGameConfig config)
        {
            float deltaGreen = config.Green / 2.0f;
            float deltaYellow = config.Yellow / 2.0f;
            _regions.RedYellow = 0.5f - deltaGreen - deltaYellow;
            _regions.YellowGreen = 0.5f - deltaGreen;
            _regions.GreenYellow = 0.5f + deltaGreen;
            _regions.YellowRed = 0.5f + deltaGreen + deltaYellow;
            
            return  _regions;
        }

        private int GetResult()
        {
            int result = Results[Result.Green];
            if (_value < _regions.RedYellow || _value > _regions.YellowRed)
            {
                result = Results[Result.Red];
            }
            else if (_value < _regions.YellowGreen || _value > _regions.GreenYellow)
            {
                result = Results[Result.Yellow];
            }

            return result;
        }

        private void StartTimer()
        {
            _timer.OnCurrentTimeChanged += OnCurrentTimeChanged;
            _timer.OnEnded += OnTimerEnded;
            _timer.Start();
        }

        private void StopTimer()
        {
            _timer.Stop();
            _timer.OnCurrentTimeChanged -= OnCurrentTimeChanged;
            _timer.OnEnded -= OnTimerEnded;
        }
    }
}