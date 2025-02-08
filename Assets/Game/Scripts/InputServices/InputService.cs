using System;
using JetBrains.Annotations;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.InputServices
{
    [UsedImplicitly]
    public sealed class InputService : 
        IShootInput, 
        IMoveInput,
        IJumpInput,
        IBlockInput,
        IActionInput,
        IDodgeInput,
        IMouseInput,
        IPauseInput,
        ITickable
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

        void ITickable.Tick()
        {
            CheckPause();
            CheckCharacterManagement();
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
            
            OnMove?.Invoke(new Vector2(horizontal, vertical).normalized);
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
            
            OnDodge?.Invoke(new Vector2(horizontal, vertical).normalized);
        }
    }
}