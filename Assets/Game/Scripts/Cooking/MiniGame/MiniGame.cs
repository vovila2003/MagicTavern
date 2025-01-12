using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Tavern.Cooking.MiniGame
{
    [UsedImplicitly]
    public class MiniGame : 
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        public event Action<Regions> OnRegionsChanged;
        public event Action<float> OnValueChanged;
        public event Action<int> OnResult;
        
        private readonly ISpaceInput _spaceInput;
        private bool _isEnable;
        private float _value;
        private int _factor;
        private float _speed;
        private Regions _regions;

        public MiniGame(ISpaceInput inputService)
        {
            _spaceInput = inputService;
            _isEnable = false;
        }

        public void CreateGame(MiniGameConfig config)
        {
            SetZones(config);
            _speed = config.SpeedValue;
        } 

        public void StartGame()
        {
            _isEnable = true;
            _factor = 1;
            _value = 0;
            
            _spaceInput.OnSpace += OnStop;
        }

        private void OnStop()
        {
            _isEnable = false;
            _spaceInput.OnSpace -= OnStop;

            int result = GetResult();
            OnResult?.Invoke(result);
        }

        void ITickable.Tick()
        {
            if (!_isEnable) return;
            
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

        void IPauseGameListener.OnPause() => _isEnable = false;

        void IResumeGameListener.OnResume() => _isEnable = true;

        private void SetZones(MiniGameConfig config)
        {
            float deltaGreen = config.Green / 2.0f;
            float deltaYellow = config.Yellow / 2.0f;
            _regions.RedYellow = 0.5f - deltaGreen - deltaYellow;
            _regions.YellowGreen = 0.5f - deltaGreen;
            _regions.GreenYellow = 0.5f + deltaGreen;
            _regions.YellowRed = 0.5f + deltaGreen + deltaYellow;
            
            OnRegionsChanged?.Invoke(_regions);
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