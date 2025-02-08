using JetBrains.Annotations;
using Tavern.Infrastructure;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class CommonPresentersFactory
    {
        private readonly GameCycleController _gameCycleController;

        public CommonPresentersFactory(GameCycleController gameCycleController)
        {
            _gameCycleController = gameCycleController;
        }

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView, UiManager uiManager) => 
            new(mainMenuView, _gameCycleController, uiManager);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView, UiManager uiManager) => 
            new(pauseView, _gameCycleController, uiManager);
    }
}