using JetBrains.Annotations;
using Tavern.Architecture;
using Tavern.Cooking;
using Tavern.UI.Views;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class PresentersFactory
    {
        private readonly ViewsFactory _viewsFactory;
        private readonly DishCookbookContext _dishCookbook;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        private readonly PauseGameController _pauseGameController;

        public PresentersFactory(
            ViewsFactory viewsViewsFactory, 
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

        public RecipeCardPresenter CreateRecipeCardPresenter()
        {
            return new RecipeCardPresenter(_viewsFactory.CreateEntityCardView());
        }

        public LeftGridRecipesPresenter CreateLeftGridPresenter()
        {
            return new LeftGridRecipesPresenter(_viewsFactory.CreateLeftGridView(), this, _dishCookbook);
        }

        public MainMenuPresenter CreateMainMenuPresenter(MainMenuView mainMenuView)
        {
            return new MainMenuPresenter(mainMenuView, _startGameController, _quitGameController);
        }

        public HudPresenter CreateHudPresenter(HudView hudView)
        {
            return new HudPresenter(hudView);
        }

        public PausePresenter CreatePausePresenter(PauseView pauseView)
        {
            return new PausePresenter(pauseView, _pauseGameController);
        }
    }
}