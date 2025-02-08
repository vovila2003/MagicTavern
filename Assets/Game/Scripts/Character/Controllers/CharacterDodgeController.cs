using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    public sealed class CharacterDodgeController : 
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        ITickable
    {
        private readonly IDodgeInput _dodgeInput;
        private Vector2 _direction;
        private readonly ICharacter _character;
        private bool _enable;

        public CharacterDodgeController(ICharacter character, IDodgeInput dodgeInput)
        {
            _character = character;
            _dodgeInput = dodgeInput;
        }

        void ITickable.Tick()
        {
            if (!_enable) return;
            
            //...
        }

        private void OnDodge(Vector2 direction)
        {
            _direction = direction;
            if (_direction == Vector2.zero) return;
            
            Debug.Log($"Dodge to {_direction}");
        }

        void IStartGameListener.OnStart() => Activate();

        void IFinishGameListener.OnFinish() => Deactivate();

        void IPauseGameListener.OnPause() => Deactivate();

        void IResumeGameListener.OnResume() => Activate();

        private void Activate()
        {
            _dodgeInput.OnDodge += OnDodge;
            _enable = true;
        }

        private void Deactivate()
        {
            _dodgeInput.OnDodge -= OnDodge;
            _enable = false;
        }
    }
}