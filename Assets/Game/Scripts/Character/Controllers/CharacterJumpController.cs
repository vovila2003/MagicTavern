using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    public sealed class CharacterJumpController : 
        IStartGameListener,
        IFinishGameListener,
        ITickable
    {
        private readonly ICharacter _character;
        private readonly IJumpInput _jumpInput;
        private bool _jumpRequired;

        public CharacterJumpController(ICharacter character, IJumpInput jumpInput)
        {
            _character = character;
            _jumpInput = jumpInput;
        }
        
        void ITickable.Tick()
        {
            if (!_jumpRequired) return;

            Debug.Log("Jump");
            _jumpRequired = false;
        }

        private void OnJump()
        {
            _jumpRequired = true;
        }

        void IStartGameListener.OnStart()
        {
            _jumpInput.OnJump += OnJump;
        }

        void IFinishGameListener.OnFinish()
        {
            _jumpInput.OnJump -= OnJump;
        }
    }
}