using Tavern.Architecture.GameManager.Controllers;
using Tavern.Cameras;
using Tavern.Character.Agents;
using Tavern.Character.Controllers;
using Tavern.Character.Visual;
using Tavern.Components;
using Tavern.InputServices;
using Tavern.Settings;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Architecture.LifetimeScopes
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] 
        private GameSettings GameSettings;

        [SerializeField] 
        private Transform World;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCommon(builder);
            RegisterCharacter(builder);
            RegisterGame(builder);
            RegisterUi(builder);
            RegisterGameCursor(builder);
            RegisterCamera(builder);
        }

        private void RegisterCommon(IContainerBuilder builder)
        {
            builder.Register<MovableByRigidbody>(Lifetime.Transient).AsImplementedInterfaces();
        }

        private void RegisterCharacter(IContainerBuilder builder)
        {
            Character.Character character = Instantiate(GameSettings.CharacterSettings.Prefab, World);

            builder.RegisterComponent(character).AsImplementedInterfaces();
            builder.RegisterInstance(GameSettings.CharacterSettings);
            builder.Register<CharacterAttackAgent>(Lifetime.Singleton);
            builder.Register<CharacterMoveController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterJumpController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterFireController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterBlockController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterActionController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterDodgeController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterAnimatorController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterGame(IContainerBuilder builder)
        {
            builder.Register<GameManager.GameManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<FinishGameController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PauseGameController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<QuitGameController>(Lifetime.Singleton);
            builder.Register<StartGameController>(Lifetime.Singleton);
        }

        private void RegisterUi(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<UiManager>().AsImplementedInterfaces();
            builder.Register<ViewModelFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterGameCursor(IContainerBuilder builder)
        {
            builder.RegisterInstance(GameSettings.CursorSettings);
            builder.Register<GameCursor.GameCursor>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterCamera(IContainerBuilder builder)
        {
            builder.RegisterInstance(GameSettings.CameraSettings);
            builder.RegisterComponentInHierarchy<CameraSetup>();
        }
    }
}
