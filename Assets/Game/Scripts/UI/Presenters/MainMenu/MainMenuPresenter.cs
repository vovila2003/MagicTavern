using Modules.GameCycle;

namespace Tavern.UI.Presenters
{
    public class MainMenuPresenter : BasePresenter
    {
        private readonly IMainMenuView _view;
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _uiManager;

        public MainMenuPresenter(IMainMenuView view,
            GameCycle gameCycle, 
            IUiManager uiManager
            ) : base(view)
        {
            _view = view;
            _gameCycle = gameCycle;
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
            _gameCycle.PrepareGame();
            _gameCycle.StartGame();
            
            _uiManager.ShowHud();
        }

        private void OnQuitGame()
        {
            _gameCycle.ExitGame();
        }
    }
}