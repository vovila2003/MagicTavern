using JetBrains.Annotations;
using Tavern.Cooking;
using Tavern.Infrastructure;

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

        public RecipeCardPresenter CreateRecipeCardPresenter()
        {
            return new RecipeCardPresenter(_viewsFactory.GetEntityCardView(), _viewsFactory.EntityCardViewPool);
        }

        public LeftGridRecipesPresenter CreateLeftGridPresenter()
        {
            return new LeftGridRecipesPresenter(_viewsFactory.CreateLeftGridView(), this, _dishCookbook);
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
    }
}