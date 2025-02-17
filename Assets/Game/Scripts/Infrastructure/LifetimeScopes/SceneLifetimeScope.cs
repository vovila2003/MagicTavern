using Tavern.Components;
using Tavern.InputServices;
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

        [SerializeField]
        private UISceneSettings UISceneSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputService>();
            builder.RegisterInstance(SceneSettings);
            
            Character.Character character = CreateCharacter(builder);
            
            new CharacterInstaller(GameSettings, character).Install(builder);
            new GameCycleInstaller().Install(builder);
            new UiInstaller(UISceneSettings, GameSettings).Install(builder);
            new CameraInstaller(GameSettings).Install(builder);
            new StoragesInstaller().Install(builder);
            new GardeningInstaller(GameSettings, SceneSettings).Install(builder);
            new LootingInstaller().Install(builder);
            new CookingInstaller(GameSettings).Install(builder);
            new ShoppingInstaller(GameSettings).Install(builder);
        }

        private Character.Character CreateCharacter(IContainerBuilder builder)
        {
            Character.Character character = Instantiate(
                GameSettings.CharacterSettings.Prefab, 
                SceneSettings.WorldTransform);
            if (!character.TryGetComponent(out SeederComponent seeder))
            {
                Debug.LogWarning($"Character {character.name} does not have a SeederComponent");
            }
            else
            {
                builder.RegisterComponent(seeder).AsImplementedInterfaces();
            }

            return character;
        }
    }
}