using System;
using System.Collections.Generic;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Gardening
{
    public class HarvestViewController
    {
        private enum State
        {
            Empty,
            Norm,
            Dry,
            Sick,
            DrySick,
            Lost
        }
        
        private readonly SpriteRenderer _spriteRenderer;
        private readonly ISeedbed _seedbed;
        private PlantMetadata _metadata;
        private readonly Caring _waterCaring;
        private readonly Caring _healCaring;
        private State _currentState = State.Empty;
        private Dictionary<State, Sprite[]> _sprites;

        public HarvestViewController(
            ISeedbed seedbed, 
            SpriteRenderer spriteRenderer, 
            Caring waterCaring, 
            Caring healCaring)
        {
            _seedbed = seedbed;
            _spriteRenderer = spriteRenderer;
            _waterCaring = waterCaring;
            _healCaring = healCaring;

            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnCaringChanged += OnCaringStateChanged;
            _seedbed.OnHarvestAgeChanged += OnHarvestAgeChanged;
            _seedbed.OnStateChanged += OnSeedbedStateChanged;
        }

        public void Dispose()
        {
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnCaringChanged -= OnCaringStateChanged;
            _seedbed.OnHarvestAgeChanged -= OnHarvestAgeChanged;
            _seedbed.OnStateChanged -= OnSeedbedStateChanged;
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            Debug.Log($"Harvest state changed to {state}");
            
            switch (state)
            {
                case HarvestState.NotReady:
                    Init();
                    break;
                case HarvestState.Ready:
                    _currentState = State.Norm;
                    int currentAge = _seedbed.Harvest.CurrentAge;
                    _spriteRenderer.sprite = _sprites[_currentState][currentAge];
                    break;
                case HarvestState.Lost:
                    Debug.Log($"Lost by reason {_seedbed.LostReason.CaringName}");
                    _currentState = State.Lost;
                    _spriteRenderer.sprite = _metadata.Lost;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Init()
        {
            _currentState = State.Norm;
            _metadata = _seedbed.Harvest.PlantConfig.PlantMetadata;
            _sprites = new Dictionary<State, Sprite[]>
            {
                {State.Norm, _metadata.Healthy},
                {State.Dry, _metadata.Drying},
                {State.DrySick, _metadata.Drying},
                {State.Sick, _metadata.Sick}
            };
        }

        private void OnCaringStateChanged(Caring caring, CaringState caringState)
        {
            if (_seedbed.Harvest.IsReady) return;
            if (_currentState is State.Lost or State.Empty) return;
            
            Debug.Log($"Care {caring.CaringName} state changed to {caringState}!");
            
            ChangeState(caring, caringState);
            
            int currentAge = _seedbed.Harvest.CurrentAge;
            _spriteRenderer.sprite = _sprites[_currentState][currentAge];
        }

        private void ChangeState(Caring caring, CaringState caringState)
        {
            switch (_currentState)
            {
                case State.Norm:
                    ChangeStateFromNorm(caring, caringState);
                    return;
                case State.Dry:
                    ChangeStateFromDry(caring, caringState);
                    break;
                case State.Sick:
                    ChangeStateFromSick(caring, caringState);
                    break;
                case State.DrySick:
                    ChangeStateFromDrySick(caring, caringState);
                    break;
                case State.Empty:
                case State.Lost:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeStateFromNorm(Caring caring, CaringState caringState)
        {
            if (caringState != CaringState.Need) return;
            
            if (caring == _waterCaring)
            {
                _currentState = State.Dry;
            }

            if (caring == _healCaring)
            {
                _currentState = State.Sick;
            }
        }

        private void ChangeStateFromDry(Caring caring, CaringState caringState)
        {
            if (caring == _waterCaring && caringState == CaringState.Norm)
            {
                _currentState = State.Norm;
            }

            if (caring == _healCaring && caringState == CaringState.Need)
            {
                _currentState = State.DrySick;
            }
        }

        private void ChangeStateFromDrySick(Caring caring, CaringState caringState)
        {
            if (caring == _waterCaring && caringState == CaringState.Norm)
            {
                _currentState = State.Sick;
            }

            if (caring == _healCaring && caringState == CaringState.Norm)
            {
                _currentState = State.Dry;
            }
        }

        private void ChangeStateFromSick(Caring caring, CaringState caringState)
        {
            if (caring == _waterCaring && caringState == CaringState.Need)
            {
                _currentState = State.DrySick;
            }

            if (caring == _healCaring && caringState == CaringState.Norm)
            {
                _currentState = State.Norm;
            }
        }

        private void OnHarvestAgeChanged(int age)
        {
            if (_currentState == State.Empty) return;
            
            Debug.Log($"Harvest age changed to {age}");
            
            _spriteRenderer.sprite = _sprites[_currentState][age];
        }

        private void OnSeedbedStateChanged(SeedbedState state)
        {
            if (state == SeedbedState.Seeded) return;
            
            _currentState = State.Empty;
            _spriteRenderer.sprite = null;
        }
    }
}