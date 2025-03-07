using JetBrains.Annotations;
using Modules.GameCycle;
using Tavern.UI;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class FinishGameController 
    {
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _manager;

        public FinishGameController(GameCycle gameCycle, IUiManager manager)
        {
            _gameCycle = gameCycle;
            _manager = manager;
        }

        public void FinishGame()
        {
            _gameCycle.FinishGame();
            _manager.ShowMainMenu();
        }
    }
}