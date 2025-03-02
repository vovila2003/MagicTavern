using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public sealed class GameCursorSettings 
    {
        [field: SerializeField] 
        public Texture2D NormalCursor { get; private set; }

        [field: SerializeField] 
        public Texture2D ShootCursor { get; private set; }
    }
}