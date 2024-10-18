using Tavern.Architecture.GameManager.Interfaces;
using Tavern.InputServices.Interfaces;

namespace Tavern.Architecture.GameManager.Controllers
{
    public sealed class PauseGameController :
        IInitGameListener,
        IExitGameListener
    {
        private readonly GameManager _gameManager;
        private readonly IPauseInput _input;
        private bool _isPause;

        public PauseGameController(GameManager gameManager, IPauseInput input)
        {
            _gameManager = gameManager;
            _input = input;
        }

        void IInitGameListener.OnInit()
        {
            _input.OnPause += OnPauseResume;
            _isPause = false;
        }

        void IExitGameListener.OnExit()
        {
            _input.OnPause -= OnPauseResume;
        }

        public void OnPauseResume()
        {
            if (_isPause)
            {
                _gameManager.ResumeGame();
            }
            else
            {
                _gameManager.PauseGame();
            }
            
            _isPause = !_isPause;
        }
    }
}