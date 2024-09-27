using Architecture.Controllers;
using Character;
using Components;
using InputServices;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Architecture
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] 
        private CharacterSettings CharacterSettings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCommon(builder);
            RegisterCharacter(builder);
            RegisterGame(builder);
            RegisterUi(builder);
        }

        private void RegisterCommon(IContainerBuilder builder)
        {
            builder.Register<MovableByTransform>(Lifetime.Transient).AsImplementedInterfaces();
        }

        private void RegisterCharacter(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(CharacterSettings.CharacterPrefab, Lifetime.Singleton)
                .UnderTransform(CharacterSettings.WorldTransform)
                .AsImplementedInterfaces();

            builder.Register<CharacterAttackAgent>(Lifetime.Singleton);
            builder.Register<CharacterMoveController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterJumpController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterFireController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterBlockController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterActionController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CharacterDodgeController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterGame(IContainerBuilder builder)
        {
            builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<FinishGameController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PauseGameController>(Lifetime.Singleton);
            builder.Register<QuitGameController>(Lifetime.Singleton);
            builder.Register<StartGameController>(Lifetime.Singleton);
        }

        private void RegisterUi(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<UiManager>().AsImplementedInterfaces();
            builder.Register<ViewModelFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
