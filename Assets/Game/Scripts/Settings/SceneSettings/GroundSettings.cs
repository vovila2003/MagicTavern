using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class GroundSettings
    {
        [field: SerializeField]
        public Vector2 Center { get; private set; }
        
        [field: SerializeField]
        public int Width { get; private set; }
        
        [field: SerializeField]
        public int Heigth { get; private set; }
        
        
    }
}