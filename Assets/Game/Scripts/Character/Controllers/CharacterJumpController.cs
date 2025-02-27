using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    [UsedImplicitly]
    public sealed class CharacterJumpController : 
        IStartGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        private readonly ICharacter _character;
        private readonly IJumpInput _jumpInput;
        private bool _jumpRequired;
        private bool _enable;

        public CharacterJumpController(ICharacter character, IJumpInput jumpInput)
        {
            _character = character;
            _jumpInput = jumpInput;
        }
        
        void ITickable.Tick()
        {
            if (!_enable) return;
            
            if (!_jumpRequired) return;

            Debug.Log("Jump");
            _jumpRequired = false;
        }

        private void OnJump()
        {
            _jumpRequired = true;
        }

        void IStartGameListener.OnStart() => Activate();

        void IFinishGameListener.OnFinish() => Deactivate();

        void IPauseGameListener.OnPause() => Deactivate();

        void IResumeGameListener.OnResume() => Activate();

        private void Activate()
        {
            _jumpInput.OnJump += OnJump;
            _enable = true;
        }

        private void Deactivate()
        {
            _jumpInput.OnJump -= OnJump;
            _enable = false;
        }
    }
}