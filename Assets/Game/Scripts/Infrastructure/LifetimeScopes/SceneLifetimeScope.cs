using Tavern.Buying;
using Tavern.Cameras;
using Tavern.Character.Agents;
using Tavern.Character.Controllers;
using Tavern.Character.Visual;
using Tavern.Components;
using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using Tavern.Cooking.MiniGame.UI;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.InputServices;
using Tavern.Looting;
using Tavern.Settings;
using Tavern.Storages;
using Tavern.Storages.CurrencyStorages;
using Tavern.UI;
using Tavern.UI.Presenters;
using Tavern.UI.Views;
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
        private Transform World;

        [SerializeField] 
        private Transform Pots;

        [SerializeField]
        private UISceneSettings UISceneSettings;

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
            RegisterShopping(builder);
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
            builder.RegisterInstance(UISceneSettings);
            builder.RegisterInstance(GameSettings.UISettings);
            
            builder.RegisterComponentInHierarchy<UiManager>().AsImplementedInterfaces();
            
            builder.Register<ViewsFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PresentersFactory>(Lifetime.Singleton);
            
            builder.RegisterComponentInHierarchy<Tester>(); //TODO for test -> remove
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
            builder.Register<ProductInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<ProductInventoryContext>();
            
            builder.Register<SeedInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<SeedInventoryContext>();
            
            builder.RegisterComponentInHierarchy<WaterStorage>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<SlopsStorage>().AsImplementedInterfaces();
            
            builder.RegisterComponentInHierarchy<MoneyStorage>().AsImplementedInterfaces();
        }

        private void RegisterGardening(IContainerBuilder builder)
        {
            builder.RegisterInstance(GameSettings.SeedMakerSettings);
            builder.RegisterInstance(GameSettings.PotPrefab);
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
            
            builder.RegisterInstance(GameSettings.DishRecipes);

            builder.Register<RecipeMatcher>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<RecipeMatcherContext>();
        }

        private void RegisterMiniGames(IContainerBuilder builder)
        {
            builder.Register<MiniGameInputService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<MiniGameView>().AsImplementedInterfaces();
            
            builder.RegisterEntryPoint<MiniGame>().AsSelf();
            builder.Register<MiniGamePlayer>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<MiniGamePresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterShopping(IContainerBuilder builder)
        {
            builder.Register<GoodsBuyCondition_CanSpendMoney>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<GoodsBuyProcessor_SpendMoney>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<DishItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FertilizerItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<KitchenItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LootItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MedicineItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ProductBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SeedBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<WaterBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<GoodsBuyer>(Lifetime.Singleton).AsSelf();

            builder.RegisterComponentInHierarchy<Shop.Shop>();

        }
    }
}
