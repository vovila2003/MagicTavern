using Tavern.Cameras;
using Tavern.Character.Agents;
using Tavern.Character.Controllers;
using Tavern.Character.Visual;
using Tavern.Components;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.InputServices;
using Tavern.Looting;
using Tavern.MiniGame;
using Tavern.MiniGame.UI;
using Tavern.Settings;
using Tavern.Storages;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Architecture
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] 
        private GameSettings GameSettings;

        [SerializeField] 
        private Transform World;

        [SerializeField] 
        private Transform Pots;

        [SerializeField] 
        private Pot PotPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCommon(builder);
            RegisterCharacter(builder);
            RegisterGame(builder);
            RegisterUi(builder);
            RegisterGameCursor(builder);
            RegisterCamera(builder);
            RegisterStorages(builder);
            RegisterGardening(builder);
            RegisterLooting(builder);
            RegisterCooking(builder);
            RegisterMiniGames(builder);
        }

        private void RegisterCommon(IContainerBuilder builder)
        {
            builder.Register<MovableByRigidbody>(Lifetime.Transient).AsImplementedInterfaces();
        }

        private void RegisterCharacter(IContainerBuilder builder)
        {
            Character.Character character = Instantiate(GameSettings.CharacterSettings.Prefab, World);
            if (!character.TryGetComponent(out SeederComponent seeder))
            {
                Debug.LogWarning($"Character {character.name} does not have a SeederComponent");
            }
            else
            {
                builder.RegisterComponent(seeder).AsImplementedInterfaces();
            }

            builder.RegisterComponent(character).AsImplementedInterfaces();
            builder.RegisterInstance(GameSettings.CharacterSettings);
            builder.Register<CharacterAttackAgent>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CharacterMoveController>();
            builder.RegisterEntryPoint<CharacterJumpController>();
            builder.RegisterEntryPoint<CharacterFireController>();
            builder.RegisterEntryPoint<CharacterBlockController>();
            builder.RegisterEntryPoint<CharacterActionController>();
            builder.RegisterEntryPoint<CharacterDodgeController>();
            builder.Register<CharacterAnimatorController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<InputService>();
        }

        private void RegisterGame(IContainerBuilder builder)
        {
            builder.Register<Modules.GameCycle.GameCycle>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<GameCycleController>().AsSelf();
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

        private void RegisterStorages(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ProductsStorage>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<SeedsStorage>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<WaterStorage>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<SlopsStorage>().AsImplementedInterfaces();
        }

        private void RegisterGardening(IContainerBuilder builder)
        {
            builder.RegisterInstance(GameSettings.SeedMakerSettings);
            builder.RegisterInstance(GameSettings.PlantsCatalog);
            builder.RegisterInstance(GameSettings.PotSettings);
            builder.RegisterComponentInHierarchy<SeedMaker>();

            builder.Register<PotsController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf().WithParameter(Pots);
            builder.RegisterComponentInHierarchy<PotCreator>();
            
            RegisterMedicine(builder);
            RegisterFertilizer(builder);
        }

        private void RegisterMedicine(IContainerBuilder builder)
        {
            builder.Register<MedicineConsumer>(Lifetime.Singleton).AsSelf();
            builder.Register<MedicineInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<MedicineInventoryContext>();
            
            builder.RegisterEntryPoint<MedicineCrafter>().AsSelf();
            builder.RegisterComponentInHierarchy<MedicineCrafterContext>();
        }

        private void RegisterFertilizer(IContainerBuilder builder)
        {
            builder.Register<FertilizerConsumer>(Lifetime.Singleton).AsSelf();
            builder.Register<FertilizerInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<FertilizerInventoryContext>();
            
            builder.RegisterEntryPoint<FertilizerCrafter>().AsSelf();
            builder.RegisterComponentInHierarchy<FertilizerCrafterContext>();
        }

        private void RegisterLooting(IContainerBuilder builder)
        {
            builder.Register<LootInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<LootInventoryContext>();
        }

        private void RegisterCooking(IContainerBuilder builder)
        {
            builder.Register<KitchenInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<KitchenInventoryContext>();

            builder.RegisterEntryPoint<DishCrafter>().AsSelf();
            builder.RegisterComponentInHierarchy<DishCrafterContext>();
            
            builder.Register<DishInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<DishInventoryContext>();

            builder.RegisterComponentInHierarchy<DishCookbookContext>();
        }

        private void RegisterMiniGames(IContainerBuilder builder)
        {
            builder.RegisterInstance(GameSettings.MiniGameSettings);
            builder.Register<MiniGameInputService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<MiniGameView>().AsImplementedInterfaces();
            
            builder.RegisterEntryPoint<MiniGame.MiniGame>().AsSelf();
            builder.RegisterComponentInHierarchy<MiniGamePlayer>();
            builder.Register<MiniGamePresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
