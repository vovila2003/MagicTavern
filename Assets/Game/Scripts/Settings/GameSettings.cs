using Tavern.MiniGame;
using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(
        fileName = "GameSettings", 
        menuName = "Settings/Game Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField]
        private CharacterSettings CharacterConfig;

        [SerializeField]
        private GameCursorSettings CursorConfig;

        [SerializeField] 
        private CameraSettings CameraConfigs;

        [SerializeField]
        private GardeningSettings GardeningConfigs;

        [SerializeField] 
        private MiniGameConfig MiniGameConfig;
        
        public SeedMakerSettings SeedMakerSettings => GardeningConfigs.SeedMakerSettings;
        
        public PotSettings PotSettings => GardeningConfigs.PotSettings;
        
        public CharacterSettings CharacterSettings => CharacterConfig;
        
        public GameCursorSettings CursorSettings => CursorConfig;
        
        public CameraSettings CameraSettings => CameraConfigs;
        
        public MiniGameConfig MiniGameSettings => MiniGameConfig;
    }
}