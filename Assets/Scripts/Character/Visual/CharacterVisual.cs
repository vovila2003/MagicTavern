using Architecture.Interfaces;
using InputServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Character
{
    public sealed class CharacterVisual : 
        MonoBehaviour,
        IInitGameListener,
        IExitGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IStartGameListener,
        IFinishGameListener
    {
        [SerializeField]
        private Transform View;
        
        [SerializeField] 
        private Transform[] Rotatables;

        private IMouseInput _mouseInput;
        private Camera _camera;
        private bool _enabled;
        private Quaternion _rotation;

        [Inject]
        private void Construct(IMouseInput mouseInput)
        {
            _mouseInput = mouseInput;
        }

        private void Update()
        {
            if (!_enabled) return;

            for (var i = 0; i < Rotatables.Length; i++)
            {
                RotateToTarget(Rotatables[i]);
            }
        }

        void IInitGameListener.OnInit()
        {
            _mouseInput.OnMouse += OnMouseInput;
            _camera = Camera.main;
            _enabled = false;
        }

        void IExitGameListener.OnExit()
        {
            _mouseInput.OnMouse -= OnMouseInput;
        }

        void IPauseGameListener.OnPause()
        {
            _enabled = false;
        }

        void IResumeGameListener.OnResume()
        {
            _enabled = true;    
        }

        void IStartGameListener.OnStart()
        {
            _enabled = true;    
        }

        void IFinishGameListener.OnFinish()
        {
            _enabled = false;
        }

        private void OnMouseInput(Vector2 mousePosition)
        {
            Vector2 targetPosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector2 directionToTarget = targetPosition - (Vector2)Rotatables[0].position;
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
            float yRotation;
            (angleToTarget, yRotation) = CheckAngle(angleToTarget);
            _rotation = Quaternion.Euler(0, yRotation, angleToTarget * Mathf.Rad2Deg);
        }

        private void RotateToTarget(Transform part)
        {
            part.rotation = _rotation;
        }

        private (float, float) CheckAngle(float angleToTarget)
        {
            float yRotation = 0f;
            if (angleToTarget is > -Mathf.PI / 2 and < Mathf.PI / 2)
            {
                View.rotation = Quaternion.Euler(0f, 0f, 0f);
                
            }
            else
            {
                View.rotation = Quaternion.Euler(0f, 180f, 0f);
                yRotation = 180;
                angleToTarget = Mathf.PI - angleToTarget;
            } 
            
            return (angleToTarget, yRotation);
        }
    }
}