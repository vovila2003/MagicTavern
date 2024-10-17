using UnityEngine;

namespace Tavern.Cameras
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/Camera Settings/Camera Settings")]
    public sealed class CameraSettings : ScriptableObject
    {
        [SerializeField]
        private Vector3 CameraOffset;
        
        public Vector3 Offset => CameraOffset;
    }
}