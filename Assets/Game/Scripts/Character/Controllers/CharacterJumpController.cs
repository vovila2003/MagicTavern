using Architecture.Interfaces;
using InputServices;
using UnityEngine;

namespace Character
{
    public sealed class CharacterJumpController : 
        IStartGameListener,
        IFinishGameListener,
        IUpdateListener
    {
        private readonly ICharacter _character;
        private readonly IJumpInput _jumpInput;
        private bool _jumpRequired;

        public CharacterJumpController(ICharacter character, IJumpInput jumpInput)
        {
            _character = character;
            _jumpInput = jumpInput;
        }
        
        void IUpdateListener.OnUpdate(float _)
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