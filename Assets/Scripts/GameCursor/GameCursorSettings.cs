using System;
using UnityEngine;

namespace GameCursor
{
    [Serializable]
    public sealed class GameCursorSettings
    {
        [SerializeField] 
        private Texture2D NormalTexture;

        [SerializeField] 
        private Texture2D ShootTexture;
        
        public Texture2D NormalCursorTexture => NormalTexture;
        public Texture2D ShootCursorTexture => ShootTexture;
    }
}