using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Cooking.MiniGame
{
    [UsedImplicitly]
    public class MiniGame : 
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        public event Action<float> OnValueChanged;
        
        private float _value;
        private int _factor;
        private float _speed;
        private Regions _regions;

        public bool IsPlaying { get; private set; }

        public Regions CreateGame(MiniGameConfig config)
        {
            _speed = config.SpeedValue;
            return SetZones(config);
        } 

        public void StartGame()
        {
            IsPlaying = true;
            _factor = 1;
            _value = 0;
        }

        public int StopGame()
        {
            IsPlaying = false;
            return GetResult();
        }

        void ITickable.Tick()
        {
            if (!IsPlaying) return;
            
            _value += _factor * Time.deltaTime * _speed;
            _factor = _value switch
            {
                > 1 => -1,
                < 0 => 1,
                _ => _factor
            };
            _value = Mathf.Clamp01(_value);
            OnValueChanged?.Invoke(_value);
        }

        void IPauseGameListener.OnPause() => IsPlaying = false;

        void IResumeGameListener.OnResume() => IsPlaying = true;

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
            var result = 2;
            if (_value < _regions.RedYellow || _value > _regions.YellowRed)
            {
                result = -2;
            }
            else if (_value < _regions.YellowGreen || _value > _regions.GreenYellow)
            {
                result = -1;
            }

            return result;
        }
    }
}