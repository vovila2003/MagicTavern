using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/Camera Settings/Camera Settings")]
    public sealed class CameraSettings : ScriptableObject
    {
        [SerializeField]
        private Vector3 CameraOffset;
        
        public Vector3 Offset => CameraOffset;
    }
}