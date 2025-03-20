using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public sealed class MinimapSettings
    {
        [field: SerializeField, PreviewField]
        public Sprite MinimapImage { get; private set; }
        
        [field: SerializeField]
        public float MinimapWidthInMeters { get; private set; }
        
        [field: SerializeField]
        public RectTransform MinimapView { get; private set; }
    }
}