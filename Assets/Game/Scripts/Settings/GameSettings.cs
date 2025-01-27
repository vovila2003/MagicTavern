using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using Tavern.Gardening;
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
        private CookingSettings CookingConfigs;
        
        [SerializeField]
        private UISettings UIConfigs;
        
        public SeedMakerSettings SeedMakerSettings => GardeningConfigs.SeedMakerSettings;
        
        public Pot PotPrefab => GardeningConfigs.Pot;
        
        public CharacterSettings CharacterSettings => CharacterConfig;
        
        public GameCursorSettings CursorSettings => CursorConfig;
        
        public CameraSettings CameraSettings => CameraConfigs;
        
        public CookingSettings CookingSettings => CookingConfigs;
        public UISettings UISettings => UIConfigs;
    }
}