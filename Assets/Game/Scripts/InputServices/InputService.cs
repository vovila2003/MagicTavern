using System;
using Tavern.Architecture.GameManager.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.InputServices
{
    public sealed class InputService : 
        IShootInput, 
        IMoveInput,
        IJumpInput,
        IBlockInput,
        IActionInput,
        IDodgeInput,
        IMouseInput,
        IPauseInput,
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
        public event Action<Vector2> OnMouse;
        public event Action OnPause;

        private Vector2 _direction;
        private Vector2 _prevDirection;
        private Vector2 _dodgeDirection;
        private Vector2 _prevDodgeDirection;
        private bool _characterManagementInputEnabled;

        void IUpdateListener.OnUpdate(float _)
        {
            CheckPause();
            CheckCharacterManagement();
        }

        void IStartGameListener.OnStart()
        {
            _characterManagementInputEnabled = true;
        }

        void IFinishGameListener.OnFinish()
        {
            _characterManagementInputEnabled = false;
        }

        void IPauseGameListener.OnPause()
        {
            _characterManagementInputEnabled = false;
        }

        void IResumeGameListener.OnResume()
        {
            _characterManagementInputEnabled = true;
        }

        private void CheckPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause?.Invoke();
            }
        }

        private void CheckCharacterManagement()
        {
            if (!_characterManagementInputEnabled) return;
            
            CheckCursor();
            CheckJump();
            CheckFire();
            CheckMove();
            CheckBlock();
            CheckAction();
            CheckDodge();
        }

        private void CheckCursor()
        {
            OnMouse?.Invoke(Input.mousePosition);
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
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            _direction = new Vector2(horizontal, vertical).normalized;

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
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) return;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            _dodgeDirection = new Vector2(horizontal, vertical).normalized;
            
            if (_dodgeDirection == _prevDodgeDirection) return;

            OnDodge?.Invoke(_dodgeDirection);
            _prevDodgeDirection = _dodgeDirection;
        }
    }
}