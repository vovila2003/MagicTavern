using Architecture.Interfaces;
using Components;
using InputServices;
using UnityEngine;

namespace Character
{
    public sealed class CharacterMoveController : 
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        IUpdateListener
    {
        private IMovable _movable;
        private readonly IMoveInput _moveInput;
        private readonly ISpeedable _speedable;
        private Vector2 _direction;
        private readonly ICharacter _character;
        private bool _enabled;

        public CharacterMoveController(ICharacter character, ISpeedable speedable, 
            IMoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
            _speedable = speedable;
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            Vector2 newPosition = _direction * (deltaTime * _speedable.GetSpeed());
            if (!InBounds(newPosition))
            {
                OnMove(Vector2.zero);
            }

            if (_enabled)
            {
                _movable.OnUpdate(deltaTime);
            }
        }

        private void OnMove(Vector2 direction)
        {
            _direction = direction;
            
            _movable.Move(direction);
        }

        private bool InBounds(Vector2 newPosition)
        {
            //Check move out of bounds
            return true;
        }

        void IStartGameListener.OnStart()
        {
            _movable = _character.GetMoveComponent();
            _moveInput.OnMove += OnMove;
            _enabled = true;
        }

        void IFinishGameListener.OnFinish()
        {
            _moveInput.OnMove -= OnMove;
            _enabled = false;
        }

        void IPauseGameListener.OnPause()
        {
            _enabled = false;
        }

        void IResumeGameListener.OnResume()
        {
            _enabled = true;
        }
    }
}