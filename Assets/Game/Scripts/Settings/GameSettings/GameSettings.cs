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

        [field: SerializeField] 
        public MinimapSettings MinimapSettings { get; private set; }
        
        [field: SerializeField]
        public TimeSettings TimeSettings {get; private set;}
        
        [field: SerializeField]
        public SaveLoadSettings SaveLoadSettings {get; private set;}

        [SerializeField]
        private GardeningSettings GardeningConfigs;

        [SerializeField]
        private CookingSettings CookingConfigs;
        
        [field: SerializeField]
        public ShoppingSettings ShoppingSettings { get; private set; }
        
        [field: SerializeField]
        public EffectsSettings EffectsSettings { get; private set; }
        
        [field: SerializeField]
        public LootSettings LootSettings { get; private set; }
        
        [SerializeField]
        private UISettings UIConfigs;
        
        public GardeningSettings GardeningSettings => GardeningConfigs;
        
        public CharacterSettings CharacterSettings => CharacterConfig;
        
        public GameCursorSettings CursorSettings => CursorConfig;
        
        public CameraSettings CameraSettings => CameraConfigs;
        
        public CookingSettings CookingSettings => CookingConfigs;
        public UISettings UISettings => UIConfigs;
    }
}