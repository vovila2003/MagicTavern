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
        private readonly IInventory<ProductItem> _productInventory;
        private readonly IInventory<LootItem> _lootInventory;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        private readonly PauseGameController _pauseGameController;
        private readonly UISettings _settings;

        public PresentersFactory(
            IViewsFactory viewsViewsFactory, 
            DishCookbookContext dishCookbook,
            IInventory<ProductItem> productInventory,
            IInventory<LootItem> lootInventory,
            StartGameController startGameController,
            QuitGameController quitGameController,
            PauseGameController pauseGameController, 
            UISettings settings)
        {
            _viewsFactory = viewsViewsFactory;
            _dishCookbook = dishCookbook;
            _productInventory = productInventory;
            _lootInventory = lootInventory;
            _startGameController = startGameController;
            _quitGameController = quitGameController;
            _pauseGameController = pauseGameController;
            _settings = settings;
        }

        public RecipeCardPresenter CreateRecipeCardPresenter(Transform viewContentTransform) =>
            new(_viewsFactory.GetEntityCardView(viewContentTransform), 
                _viewsFactory.EntityCardViewPool);

        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform) =>
            new(_viewsFactory.GetItemCardView(viewContentTransform), 
                _viewsFactory.ItemCardViewPool);

        public LeftGridRecipesPresenter CreateLeftGridPresenter(Transform viewContainer) =>
            new(_viewsFactory.CreateLeftGridView(viewContainer), 
                _dishCookbook,
                this);

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView) => 
            new(mainMenuView, _startGameController, _quitGameController);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView) => 
            new(pauseView, _pauseGameController);

        public CookingPanelPresenter CreateCookingPanelPresenter() => 
            new(_viewsFactory.CreatePanelView(), this);

        public CookingMiniGamePresenter CreateCookingMiniGamePresenter(Transform viewContainer) => 
            new(_viewsFactory.CreateCookingMiniGameView(viewContainer), _settings.CookingSettings);

        public CookingIngredientsPresenter CreateCookingIngredientsPresenter(Transform viewContainer) =>
            new(_viewsFactory.CreateCookingIngredientsView(viewContainer),
                _productInventory,
                _lootInventory,
                this);

        public MatchRecipePresenter CreateMatchRecipePresenter(Transform viewContainer) => 
            new(_viewsFactory.CreateMatchRecipeView(viewContainer));
    }
}