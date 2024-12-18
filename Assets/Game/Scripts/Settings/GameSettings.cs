using Modules.Gardening;
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
        
        public SeedMakerSettings SeedMakerSettings => GardeningConfigs.SeedMakerSettings;
        
        public PlantsCatalog PlantsCatalog => GardeningConfigs.Catalog;
        
        public PotSettings PotSettings => GardeningConfigs.PotSettings;
        
        public CharacterSettings CharacterSettings => CharacterConfig;
        
        public GameCursorSettings CursorSettings => CursorConfig;
        
        public CameraSettings CameraSettings => CameraConfigs;
    }
}