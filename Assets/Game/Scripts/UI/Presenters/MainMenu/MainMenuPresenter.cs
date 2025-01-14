using Tavern.Architecture;

namespace Tavern.UI.Presenters
{
    public class MainMenuPresenter
    {
        private readonly IMainMenuView _view;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        
        public MainMenuPresenter(
            IMainMenuView view,
            StartGameController startGameController, 
            QuitGameController quitGameController)
        {
            _view = view;
            _startGameController = startGameController;
            _quitGameController = quitGameController;
        }

        public void Show()
        {
            _view.OnStartGame += OnStartGame; 
            _view.OnQuitGame += OnQuitGame; 
            _view.Show();
        }

        public void Hide()
        {
            _view.OnStartGame -= OnStartGame; 
            _view.OnQuitGame -= OnQuitGame; 
            _view.Hide();
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