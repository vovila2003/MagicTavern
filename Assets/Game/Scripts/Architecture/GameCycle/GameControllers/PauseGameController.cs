using Tavern.Architecture.GameManager.Interfaces;
using Tavern.InputServices.Interfaces;

namespace Tavern.Architecture
{
    public sealed class PauseGameController :
        IInitGameListener,
        IExitGameListener
    {
        private readonly GameCycleController _gameCycle;
        private readonly IPauseInput _input;
        private bool _isPause;

        public PauseGameController(GameCycleController gameCycle, IPauseInput input)
        {
            _gameCycle = gameCycle;
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
                _gameCycle.ResumeGame();
            }
            else
            {
                _gameCycle.PauseGame();
            }
            
            _isPause = !_isPause;
        }
    }
}