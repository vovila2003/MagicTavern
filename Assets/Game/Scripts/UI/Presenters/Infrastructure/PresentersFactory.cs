using JetBrains.Annotations;
using Tavern.Cooking;
using Tavern.Infrastructure;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class PresentersFactory
    {
        private readonly IViewsFactory _viewsFactory;
        private readonly DishCookbookContext _dishCookbook;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        private readonly PauseGameController _pauseGameController;

        public PresentersFactory(
            IViewsFactory viewsViewsFactory, 
            DishCookbookContext dishCookbook,
            StartGameController startGameController,
            QuitGameController quitGameController,
            PauseGameController pauseGameController)
        {
            _viewsFactory = viewsViewsFactory;
            _dishCookbook = dishCookbook;
            _startGameController = startGameController;
            _quitGameController = quitGameController;
            _pauseGameController = pauseGameController;
        }

        public RecipeCardPresenter CreateRecipeCardPresenter(Transform viewContentTransform)
        {
            return new RecipeCardPresenter(
                _viewsFactory.GetEntityCardView(viewContentTransform), 
                _viewsFactory.EntityCardViewPool);
        }
        
        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform)
        {
            return new ItemCardPresenter(
                _viewsFactory.GetItemCardView(viewContentTransform), 
                _viewsFactory.ItemCardViewPool);
        }

        public LeftGridRecipesPresenter CreateLeftGridPresenter(Transform viewContainer)
        {
            return new LeftGridRecipesPresenter(
                _viewsFactory.CreateLeftGridView(viewContainer), 
                this, 
                _dishCookbook);
        }

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView)
        {
            return new MainMenuPresenter(mainMenuView, _startGameController, _quitGameController);
        }

        public HudPresenter CreateHudPresenter(IHudView hudView)
        {
            return new HudPresenter(hudView);
        }

        public PausePresenter CreatePausePresenter(IPauseView pauseView)
        {
            return new PausePresenter(pauseView, _pauseGameController);
        }

        public CookingPanelPresenter CreateCookingPanelPresenter()
        {
            return new CookingPanelPresenter(_viewsFactory.CreatePanelView(), this);
        }

        public CookingMiniGamePresenter CreateCookingMiniGamePresenter(Transform viewContainer)
        {
            return new CookingMiniGamePresenter(_viewsFactory.CreateCookingMiniGameView(viewContainer));
        }
        
        public CookingIngredientsPresenter CreateCookingIngredientsPresenter(Transform viewContainer)
        {
            return new CookingIngredientsPresenter(_viewsFactory.CreateCookingIngredientsView(viewContainer));
        }
    }
}