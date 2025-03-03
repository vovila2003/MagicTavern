using Tavern.Character.Agents;
using Tavern.Character.Controllers;
using Tavern.Character.Visual;
using Tavern.Components;
using UnityEngine;
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
            if (!_character.TryGetComponent(out SeederComponent seeder))
            {
                Debug.LogWarning($"Character {_character.name} does not have a SeederComponent");
            }
            else
            {
                builder.RegisterComponent(seeder).AsSelf();
            }
            
            builder.RegisterComponent(_character).AsImplementedInterfaces();
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