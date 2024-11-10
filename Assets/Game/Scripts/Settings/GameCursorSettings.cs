using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(fileName = "CursorSettings", menuName = "Settings/Cursor Settings/Cursor Settings")]
    public sealed class GameCursorSettings : ScriptableObject
    {
        [SerializeField]
        private Texture2D NormalCursorTexture;

        [SerializeField]
        private Texture2D ShootCursorTexture;
        
        public Texture2D NormalCursor => NormalCursorTexture;
        public Texture2D ShootCursor => ShootCursorTexture;
    }
}