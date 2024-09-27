using Architecture.Controllers;

namespace UI
{
    public sealed class HudViewModel : IHudViewModel
    {
        private readonly PauseGameController _pauseGameController;

        public HudViewModel(PauseGameController pauseGameController)
        {
            _pauseGameController = pauseGameController;
        }

        public void Pause()
        {
            _pauseGameController.OnPauseResume();            
        }
    }
}