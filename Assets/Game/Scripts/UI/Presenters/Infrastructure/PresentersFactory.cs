using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Infrastructure;
using Tavern.Looting;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class PresentersFactory
    {
        private readonly IViewsFactory _viewsFactory;
        private readonly DishCookbookContext _dishCookbook;
        private readonly IStackableInventory<ProductItem> _productInventory;
        private readonly IStackableInventory<LootItem> _lootInventory;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly DishCrafter _dishCrafter;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        private readonly PauseGameController _pauseGameController;
        private readonly UISettings _settings;
        private readonly UISceneSettings _sceneSettings;

        public PresentersFactory(
            IViewsFactory viewsViewsFactory, 
            DishCookbookContext dishCookbook,
            IStackableInventory<ProductItem> productInventory,
            IStackableInventory<LootItem> lootInventory,
            ActiveDishRecipe activeDishRecipe,
            DishCrafter dishCrafter,
            StartGameController startGameController,
            QuitGameController quitGameController,
            PauseGameController pauseGameController, 
            UISettings settings,
            UISceneSettings sceneSettings)
        {
            _viewsFactory = viewsViewsFactory;
            _dishCookbook = dishCookbook;
            _productInventory = productInventory;
            _lootInventory = lootInventory;
            _activeDishRecipe = activeDishRecipe;
            _dishCrafter = dishCrafter;
            _startGameController = startGameController;
            _quitGameController = quitGameController;
            _pauseGameController = pauseGameController;
            _settings = settings;
            _sceneSettings = sceneSettings;
        }

        public RecipeCardPresenter CreateRecipeCardPresenter(Transform viewContentTransform) =>
            new(_viewsFactory.GetEntityCardView(viewContentTransform), 
                _viewsFactory.EntityCardViewPool);

        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform) =>
            new(_viewsFactory.GetItemCardView(viewContentTransform), 
                _viewsFactory.ItemCardViewPool);

        public CookingRecipesPresenter CreateLeftGridPresenter(Transform viewContainer) =>
            new(_viewsFactory.CreateLeftGridView(viewContainer), 
                _dishCookbook,
                this);

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView) => 
            new(mainMenuView, _startGameController, _quitGameController);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView) => 
            new(pauseView, _pauseGameController);

        public CookingPanelPresenter CreateCookingPanelPresenter() => 
            new(_viewsFactory.CreatePanelView(),
                this,
                _activeDishRecipe);

        public CookingAndMatchRecipePresenter CreateCookingAndMatchRecipePresenter(
            Transform viewContainer, ActiveDishRecipe recipe) => 
            new(_viewsFactory.CreateCookingAndMatchRecipeView(viewContainer), this, recipe);

        public CookingIngredientsPresenter CreateCookingIngredientsPresenter(Transform viewContainer) =>
            new(_viewsFactory.CreateCookingIngredientsView(viewContainer),
                _productInventory,
                _lootInventory,
                this,
                _sceneSettings.Canvas);

        public MatchNewRecipePresenter CreateMatchNewRecipePresenter(Transform viewContainer) => 
            new(_viewsFactory.CreateMatchNewRecipeView(viewContainer));

        public ItemInfoPresenter CreateItemInfoPresenter(Transform parent) =>
            new(_viewsFactory.InfoViewProvider, parent);

        public CookingMiniGamePresenter CreateCookingMiniGamePresenter(Transform parent) =>
            new(_viewsFactory.CreateCookingMiniGameView(parent), _dishCrafter);

        public RecipeIngredientsPresenter CreateRecipeIngredientsPresenter(Transform parent, ActiveDishRecipe recipe) => 
            new(_viewsFactory.CreateRecipeIngredientsView(parent), 
                _settings.CookingSettings, 
                CreateItemInfoPresenter,
                _sceneSettings.Canvas,
                recipe);

        public RecipeEffectsPresenter CreateRecipeEffectsPresenter(Transform parent) => 
            new(_viewsFactory.CreateRecipeEffectsView(parent), _settings.CookingSettings);
    }
}