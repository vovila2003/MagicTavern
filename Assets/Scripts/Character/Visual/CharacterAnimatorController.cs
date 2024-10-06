using System;
using Architecture.Interfaces;
using InputServices;
using UnityEngine;

namespace Character
{
    public class CharacterAnimatorController : 
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener
    {
        private const float Threshold = 0.05f;
        private static readonly int MoveUpDown = Animator.StringToHash("moveUpDown");
        private static readonly int MoveLeftRight = Animator.StringToHash("moveLeftRight");
        private ICharacter _character;
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
            _moveInput.OnMove += OnMove;
            _animator.enabled = true;
        }

        void IFinishGameListener.OnFinish()
        {
            _moveInput.OnMove -= OnMove;
            _animator.enabled = false;
        }

        void IPauseGameListener.OnPause()
        {
            _animator.enabled = false;
        }

        void IResumeGameListener.OnResume()
        {
            _animator.enabled = true;
        }

        private void OnMove(Vector2 direction)
        {
            float upDown = 0;
            float leftRight = 0;
            if (Mathf.Abs(direction.y) > Threshold)
            {
                upDown = direction.y;
            } 
            else if (Mathf.Abs(direction.x) > Threshold)
            {
                leftRight = direction.x;
            }
            
            _animator.SetFloat(MoveUpDown, upDown);
            _animator.SetFloat(MoveLeftRight, leftRight);
        }
    }
}