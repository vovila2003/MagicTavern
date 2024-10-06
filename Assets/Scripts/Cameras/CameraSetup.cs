using Character;
using Cinemachine;
using UnityEngine;
using VContainer;

namespace Cameras
{
    public class CameraSetup : MonoBehaviour
    {
        private ICinemachineCamera _camera;
        private Transform _characterTransform;
        private CameraSettings _cameraSettings;

        [Inject]
        private void Construct(ICharacter character, CameraSettings settings)
        {
            _characterTransform = character.GetTransform();
            _cameraSettings = settings;
        }

        private void Start()
        {
            _camera = Camera.main?.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
            if (_camera == null)
            {
                Debug.LogWarning("Camera not found");
                return;
            }
            
            _camera.Follow = _characterTransform;
        }
    }
}