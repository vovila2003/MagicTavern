using Architecture.Controllers;

namespace UI
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        
        public MainMenuViewModel(StartGameController startGameController, 
            QuitGameController quitGameController)
        {
            _startGameController = startGameController;
            _quitGameController = quitGameController;
        }

        public void StartGame()
        {
            _startGameController.OnStart();
        }

        public void QuitGame()
        {
            _quitGameController.OnQuit();
        }
    }
}