using Architecture.Controllers;

namespace UI
{
    public sealed class PauseViewModel : IPauseViewModel
    {
        private readonly PauseGameController _pauseGameController;

        public PauseViewModel(PauseGameController pauseGameController)
        {
            _pauseGameController = pauseGameController;
        }

        public void Resume()
        {
            _pauseGameController.OnPauseResume();            
        }
    }
}