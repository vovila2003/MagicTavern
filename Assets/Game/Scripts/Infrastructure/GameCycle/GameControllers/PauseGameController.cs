using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using Tavern.UI;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class PauseGameController :
        IInitGameListener,
        IExitGameListener
    {
        private readonly GameCycleController _gameCycle;
        private readonly IPauseInput _input;
        private readonly UiManager _uiManager;

        public PauseGameController(GameCycleController gameCycle, 
            IPauseInput input, 
            UiManager uiManager)
        {
            _gameCycle = gameCycle;
            _input = input;
            _uiManager = uiManager;
        }

        void IInitGameListener.OnInit()
        {
            _input.OnPause += OnPauseResume;
        }

        void IExitGameListener.OnExit()
        {
            _input.OnPause -= OnPauseResume;
        }

        private void OnPauseResume()
        {
            GameState state = _gameCycle.GameState;

            if (state != GameState.Pause && state != GameState.IsRunning) return;

            if (state == GameState.Pause)
            {
                if (_uiManager.IsOpen)
                {
                    _uiManager.HideUi();
                    return;
                }
                
                _gameCycle.ResumeGame();
                _uiManager.HidePause();
            }

            if (state != GameState.IsRunning) return;
            
            _gameCycle.PauseGame();
            _uiManager.ShowPause();
        }
    }
}