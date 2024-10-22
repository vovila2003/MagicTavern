using Tavern.Cameras;
using Tavern.Character;
using Tavern.GameCursor;
using Tavern.Gardening;
using UnityEngine;

namespace Tavern.Settings
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

        [SerializeField]
        public SeedMakerSettings SeedMakerConfig;
        
        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        
        public CharacterSettings CharacterSettings => CharacterConfig;
        
        public GameCursorSettings CursorSettings => CursorConfig;
        
        public CameraSettings CameraSettings => CameraConfigs;
    }
}