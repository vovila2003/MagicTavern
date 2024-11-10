using Cinemachine;
using Tavern.Character;
using Tavern.Settings;
using UnityEngine;
using VContainer;

namespace Tavern.Cameras
{
    public class CameraSetup : MonoBehaviour
    {
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
            ICinemachineCamera cinemachineCamera = Camera.main?.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
            if (cinemachineCamera == null)
            {
                Debug.LogWarning("Camera not found");
                return;
            }
            
            cinemachineCamera.Follow = _characterTransform;

            CinemachineTransposer transposer = cinemachineCamera
                .VirtualCameraGameObject
                .GetComponentInChildren<CinemachineTransposer>();
            if (transposer == null) return;
            
            transposer.m_FollowOffset = _cameraSettings.Offset;
        }
    }
}