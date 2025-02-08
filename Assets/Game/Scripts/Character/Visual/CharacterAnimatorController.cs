using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.Character.Visual
{
    [UsedImplicitly]
    public class CharacterAnimatorController : 
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener
    {
        private static readonly int MoveUpDown = Animator.StringToHash("moveUpDown");
        private static readonly int MoveLeftRight = Animator.StringToHash("moveLeftRight");
        private readonly ICharacter _character;
        private readonly IMoveInput _moveInput;
        private Animator _animator;
        
        public CharacterAnimatorController(ICharacter character, IMoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
        }

        void IStartGameListener.OnStart()
        {
            _animator = _character.GetAnimator();
            if (_animator == null)
            {
                Debug.LogError($"{nameof(CharacterAnimatorController)} has no animator");
            }
            
            Activate();
        }

        void IFinishGameListener.OnFinish() => Deactivate();

        void IPauseGameListener.OnPause() => Deactivate();

        void IResumeGameListener.OnResume() => Activate();

        private void OnMove(Vector2 direction)
        {
            _animator.SetFloat(MoveUpDown, direction.y);
            _animator.SetFloat(MoveLeftRight, direction.x);
        }

        private void Activate()
        {
            _moveInput.OnMove += OnMove;
            _animator.enabled = true;
        }

        private void Deactivate()
        {
            _moveInput.OnMove -= OnMove;
            _animator.enabled = false;
        }
    }
}