using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public sealed class MinimapSettings
    {
        [field: SerializeField, PreviewField]
        public Sprite MiniMap { get; private set; }
        
        [field: SerializeField] 
        public Vector3 MinimapCameraOffset { get; private set; }
    }
}