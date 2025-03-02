using Tavern.Infrastructure;
using Tavern.UI;

namespace Tavern.Gardening
{
    public class PotListener
    {
        private readonly Pot _pot;
        private readonly GameCycleController _gameCycleController;
        private readonly IUiManager _uiManager;

        public PotListener(
            Pot pot, 
            GameCycleController gameCycleController,
            IUiManager uiManager
        )
        {
            _pot = pot;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;

            _pot.OnActivated += PotContextActivated;
        }

        public void Dispose()
        {
            _pot.OnActivated -= PotContextActivated;
        }
        
        private void PotContextActivated()
        {
            _gameCycleController.PauseGame();
            _pot.OnActivated -= PotContextActivated;
            _uiManager.ShowGardeningUi(_pot, OnExitGardeningUi);
        }
        
        private void OnExitGardeningUi()
        {
            _gameCycleController.ResumeGame();
            _pot.OnActivated += PotContextActivated;
        }
    }
}