using Tavern.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] 
        private GameSettings GameSettings;

        [SerializeField] 
        private SceneSettings SceneSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            new InputInstaller().Install(builder);
            new SettingsInstaller(GameSettings, SceneSettings).Install(builder);

            new CharacterInstaller(Instantiate(
                GameSettings.CharacterSettings.CharacterPrefab, 
                SceneSettings.WorldTransform)).Install(builder);
            
            new GameCycleInstaller().Install(builder);
            new UiInstaller().Install(builder);
            new CameraInstaller().Install(builder);
            new StoragesInstaller().Install(builder);
            new GardeningInstaller().Install(builder);
            new LootingInstaller().Install(builder);
            new CookingInstaller().Install(builder);
            new ShoppingInstaller().Install(builder);
            new DebugInstaller().Install(builder);
            
        }
    }
}