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
        IFixedUpdateListener
    {
        private IMovable _movable;
        private readonly IMoveInput _moveInput;
        private readonly ISpeedable _speedable;
        private Vector3 _direction;
        private readonly ICharacter _character;
        private bool _enabled;

        public CharacterMoveController(ICharacter character, ISpeedable speedable, 
            IMoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
            _speedable = speedable;
        }

        void IFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            Vector3 newPosition = _direction * (fixedDeltaTime * _speedable.GetSpeed());
            if (!InBounds(newPosition))
            {
                OnMove(Vector2.zero);
            }

            if (_enabled)
            {
                _movable.OnFixedUpdate(fixedDeltaTime);
            }
        }

        private void OnMove(Vector2 direction)
        {
            _direction = new Vector3(direction.x, 0, direction.y);
            
            _movable.Move(_direction);
        }

        private bool InBounds(Vector3 newPosition)
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