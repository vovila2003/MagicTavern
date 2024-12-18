using System;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tavern.MiniGame
{
    public class MiniGame : 
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
        private float _start;
        private float _finish;
        private float _speed;

        public MiniGame(ISpaceInput inputService, MiniGameConfig config)
        {
            _config = config;
            _spaceInput = inputService;
            _isEnable = false;
        }

        public void StartGame()
        {
            SetTarget();
            SetSpeed();
            _isEnable = true;
            _spaceInput.OnSpace += OnStop;
            _factor = 1;
            _value = 0;
        }

        private void OnStop()
        {
            _isEnable = false;
            _spaceInput.OnSpace -= OnStop;
            OnResult?.Invoke(_value >= _start && _value <= _finish);
        }

        private void SetTarget()
        {
            float range = Random.Range(_config.TargetValueMin, _config.TargetValueMax);
            _start = Random.Range(0, 1 - range);
            _finish = _start + range;
            var target = new Vector2(_start, _finish);
            OnTargetChanged?.Invoke(target);
        }

        private void SetSpeed()
        {
            _speed = Random.Range(_config.SpeedValueMin, _config.SpeedValueMax);
        }

        public void Tick(float deltaTime)
        {
            if (!_isEnable) return;
            
            _value += _factor * deltaTime * _speed;
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