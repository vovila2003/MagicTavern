using JetBrains.Annotations;
using Tavern.Infrastructure;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class CommonPresentersFactory
    {
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        private readonly PauseGameController _pauseGameController;


        public CommonPresentersFactory(
            StartGameController startGameController, 
            QuitGameController quitGameController, 
            PauseGameController pauseGameController)
        {
            _startGameController = startGameController;
            _quitGameController = quitGameController;
            _pauseGameController = pauseGameController;
        }

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView) => 
            new(mainMenuView, _startGameController, _quitGameController);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView) => 
            new(pauseView, _pauseGameController);
    }
}