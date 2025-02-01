using Tavern.Infrastructure;

namespace Tavern.UI.Presenters
{
    public class MainMenuPresenter : BasePresenter
    {
        private readonly IMainMenuView _view;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        
        public MainMenuPresenter(
            IMainMenuView view,
            StartGameController startGameController, 
            QuitGameController quitGameController) : base(view)
        {
            _view = view;
            _startGameController = startGameController;
            _quitGameController = quitGameController;
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
            _startGameController.OnStart();
        }

        private void OnQuitGame()
        {
            _quitGameController.OnQuit();    
        }
    }
}