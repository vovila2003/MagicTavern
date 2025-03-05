using Tavern.Character.Agents;
using Tavern.Character.Controllers;
using Tavern.Character.Visual;
using Tavern.Components;
using Tavern.Gardening;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class CharacterInstaller : IInstaller
    {
        private readonly Character.Character _character;

        public CharacterInstaller(Character.Character character)
        {
            _character = character;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_character).AsImplementedInterfaces();
            builder.Register<Seeder>(Lifetime.Singleton).AsSelf();
            builder.Register<MovableByRigidbody>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<CharacterAttackAgent>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CharacterMoveController>();
            builder.RegisterEntryPoint<CharacterJumpController>();
            builder.RegisterEntryPoint<CharacterFireController>();
            builder.RegisterEntryPoint<CharacterBlockController>();
            builder.RegisterEntryPoint<CharacterDodgeController>();
            builder.Register<CharacterAnimatorController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}