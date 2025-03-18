using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public sealed class CameraSettings 
    {
        [field: SerializeField] 
        public Vector3 CameraOffset { get; private set; }
        
        [field: SerializeField] 
        public Vector3 MinimapCameraOffset { get; private set; }
    }
}