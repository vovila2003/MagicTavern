using System;
using Architecture.Interfaces;
using UnityEngine;

namespace InputServices
{
    public sealed class InputService : 
        IShootInput, 
        IMoveInput,
        IJumpInput,
        IBlockInput,
        IActionInput,
        IDodgeInput,
        IStartGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener, 
        IUpdateListener
    {
        public event Action OnFire;
        public event Action OnAlternativeFire;
        public event Action OnJump;
        public event Action<Vector2> OnMove;
        public event Action OnBlock;
        public event Action OnAction;
        public event Action<Vector2> OnDodge;

        private Vector2 _direction;
        private Vector2 _prevDirection;
        private Vector2 _dodgeDirection;
        private Vector2 _prevDodgeDirection;
        private bool _enabled;

        void IUpdateListener.OnUpdate(float _)
        {
            if (!_enabled) return;
            
            CheckJump();
            CheckFire();
            CheckMove();
            CheckBlock();
            CheckAction();
            CheckDodge();
        }

        void IStartGameListener.OnStart()
        {
            _enabled = true;
        }

        void IFinishGameListener.OnFinish()
        {
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

        private void CheckJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJump?.Invoke();
            }
        }

        private void CheckFire()
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
            {
                OnFire?.Invoke();
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) &&
                Input.GetMouseButtonDown(0))
            {
                OnAlternativeFire?.Invoke();
            }
        }

        private void CheckMove()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ) return;
            
            if (Input.GetKey(KeyCode.A))
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _direction = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _direction = Vector2.down;
            }
            else
            {
                _direction = Vector2.zero;
            }

            if (_direction == _prevDirection) return;
            
            OnMove?.Invoke(_direction);
            _prevDirection = _direction;
        }

        private void CheckBlock()
        {
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) &&
                Input.GetMouseButtonDown(1))
            {
                OnBlock?.Invoke();
            }
        }

        private void CheckAction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnAction?.Invoke();
            }
        }

        private void CheckDodge()
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) ) return;
            
            if (Input.GetKey(KeyCode.A))
            {
                _dodgeDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _dodgeDirection = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                _dodgeDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _dodgeDirection = Vector2.down;
            }
            else
            {
                _dodgeDirection = Vector2.zero;
            }
            
            if (_dodgeDirection == _prevDodgeDirection) return;

            OnDodge?.Invoke(_dodgeDirection);
            _prevDodgeDirection = _dodgeDirection;
        }
    }
}