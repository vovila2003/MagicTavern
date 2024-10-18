using Tavern.Architecture.GameManager.Controllers;
using Tavern.UI.ViewModels.Interfaces;

namespace Tavern.UI.ViewModels
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