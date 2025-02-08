using Tavern.Infrastructure;

namespace Tavern.UI.Presenters
{
    public class MainMenuPresenter : BasePresenter
    {
        private readonly IMainMenuView _view;
        private readonly GameCycleController _gameCycleController;
        private readonly UiManager _uiManager;

        public MainMenuPresenter(IMainMenuView view,
            GameCycleController gameCycleController, 
            UiManager uiManager
            ) : base(view)
        {
            _view = view;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;
        }

        protected override void OnShow()
        {
            _view.OnStartGame += OnStartGame; 
            _view.OnQuitGame += OnQuitGame; 
        }

        protected override void OnHide()
        {
            _view.OnStartGame -= OnStartGame; 
            _view.OnQuitGame -= OnQuitGame; 
        }

        private void OnStartGame()
        {
            _gameCycleController.PrepareGame();
            _gameCycleController.StartGame();
            
            _uiManager.ShowHud();
        }

        private void OnQuitGame()
        {
            _gameCycleController.ExitGame();
        }
    }
}