using System;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.MiniGame
{
    public class MiniGame : 
        IUpdateListener,
        IPauseGameListener,
        IResumeGameListener
    {
        public event Action<Vector2> OnTargetChanged;
        public event Action<float> OnValueChanged;
        public event Action<bool> OnResult;
        
        private readonly MiniGameConfig _config;
        private readonly ISpaceInput _spaceInput;
        private bool _isEnable;
        private float _value;
        private int _factor;

        public MiniGame(ISpaceInput inputService, MiniGameConfig config)
        {
            _config = config;
            _spaceInput = inputService;
            _isEnable = false;
        }

        public void StartGame()
        {
            SetTarget();
            _isEnable = true;
            _spaceInput.OnSpace += OnStop;
            _factor = 1;
            _value = 0;
        }

        private void OnStop()
        {
            _isEnable = false;
            _spaceInput.OnSpace -= OnStop;
            OnResult?.Invoke(_value >= _config.MinValue && _value <= _config.MaxValue);
        }

        private void SetTarget()
        {
            Vector2 target = new Vector2(_config.MinValue, _config.MaxValue);
            OnTargetChanged?.Invoke(target);
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_isEnable) return;
            
            _value += _factor * deltaTime * _config.SpeedValue;
            _factor = _value switch
            {
                > 1 => -1,
                < 0 => 1,
                _ => _factor
            };
            _value = Mathf.Clamp01(_value);
            OnValueChanged?.Invoke(_value);
        }

        public void OnPause()
        {
            _isEnable = false;
        }

        public void OnResume()
        {
            _isEnable = true;
        }
    }
}