using Cameras;
using Character;
using GameCursor;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game Settings/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField]
        private CharacterSettings CharacterConfig;

        [SerializeField]
        private GameCursorSettings CursorConfig;

        [SerializeField] 
        private CameraSettings CameraConfigs;

        public CharacterSettings CharacterSettings => CharacterConfig;
        
        public GameCursorSettings CursorSettings => CursorConfig;
        
        public CameraSettings CameraSettings => CameraConfigs;
    }
}