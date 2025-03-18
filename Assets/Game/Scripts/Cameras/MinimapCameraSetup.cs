using Modules.GameCycle.Interfaces;
using Tavern.Character;
using Tavern.Settings;
using UnityEngine;
using VContainer;

namespace Tavern.Cameras
{
    public class MinimapCameraSetup : 
        MonoBehaviour,
        IStartGameListener,
        IFinishGameListener
    {
        private Transform _characterTransform;
        private CameraSettings _cameraSettings;
        private Transform _cameraTransform;
        private bool _isActive;

        [Inject]
        private void Construct(ICharacter character, GameSettings settings)
        {
            _characterTransform = character.GetTransform();
            _cameraSettings = settings.CameraSettings;
        }

        private void Awake()
        {
            _cameraTransform = transform;
        }

        private void LateUpdate()
        {
            if (!_isActive) return;
            
            _cameraTransform.position = _characterTransform.position + _cameraSettings.MinimapCameraOffset;
        }

        public void OnStart()
        {
            _isActive = true;
        }

        public void OnFinish()
        {
            _isActive = false;
        }
    }
}