using Tavern.Architecture;
using Tavern.UI.Views;

namespace Tavern.UI.Presenters
{
    public class MainMenuPresenter
    {
        private readonly MainMenuView _view;
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        
        public MainMenuPresenter(
            MainMenuView view,
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
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.OnStartGame -= OnStartGame; 
            _view.OnQuitGame -= OnQuitGame; 
            _view.gameObject.SetActive(false);
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