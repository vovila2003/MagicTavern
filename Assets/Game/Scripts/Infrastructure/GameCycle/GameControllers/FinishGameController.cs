using JetBrains.Annotations;
using Tavern.UI;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class FinishGameController 
    {
        private readonly GameCycleController _gameCycleController;
        private readonly UiManager _manager;

        public FinishGameController(GameCycleController gameCycleController, UiManager manager)
        {
            _gameCycleController = gameCycleController;
            _manager = manager;
        }

        public void FinishGame()
        {
            _gameCycleController.FinishGame();
            _manager.ShowMainMenu();
        }
    }
}