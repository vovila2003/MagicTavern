using Architecture.Controllers;

namespace UI
{
    public sealed class ViewModelFactory : IViewModelFactory
    {
        private readonly StartGameController _startGameController;
        private readonly QuitGameController _quitGameController;
        private readonly PauseGameController _pauseGameController;

        public ViewModelFactory(StartGameController startGameController, 
            QuitGameController quitGameController,
            PauseGameController pauseGameController)
        {
            _startGameController = startGameController;
            _quitGameController = quitGameController;
            _pauseGameController = pauseGameController;
        }
        
        public IMainMenuViewModel CreateMainMenuViewModel() => 
            new MainMenuViewModel(_startGameController, _quitGameController);

        public IPauseViewModel CreatePauseViewModel() => 
            new PauseViewModel(_pauseGameController);

        public IHudViewModel CreateHudViewModel() => 
            new HudViewModel(_pauseGameController);
    }
}