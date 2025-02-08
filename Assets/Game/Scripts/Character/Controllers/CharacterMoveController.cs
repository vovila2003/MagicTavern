using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Components.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    [UsedImplicitly]
    public sealed class CharacterMoveController : 
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        IFixedTickable
    {
        private IMovable _movable;
        private readonly IMoveInput _moveInput;
        private readonly ISpeedable _speedable;
        private Vector3 _direction;
        private readonly ICharacter _character;
        private bool _enable;

        public CharacterMoveController(ICharacter character, ISpeedable speedable, 
            IMoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
            _speedable = speedable;
        }

        void IFixedTickable.FixedTick()
        {
            if (!_enable) return;
            
            float fixedDeltaTime = Time.fixedDeltaTime;
            Vector3 newPosition = _direction * (fixedDeltaTime * _speedable.GetSpeed());
            if (!InBounds(newPosition))
            {
                OnMove(Vector2.zero);
            }

            _movable.OnFixedUpdate(fixedDeltaTime);
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
            Activate();
        }

        void IFinishGameListener.OnFinish() => Deactivate();

        void IPauseGameListener.OnPause()
        {
            Deactivate();
            OnMove(Vector2.zero);
        }

        void IResumeGameListener.OnResume() => Activate();

        private void Activate()
        {
            _moveInput.OnMove += OnMove;
            _enable = true;
        }

        private void Deactivate()
        {
            _moveInput.OnMove -= OnMove;
            _enable = false;
        }
    }
}