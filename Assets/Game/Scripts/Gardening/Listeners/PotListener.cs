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

            _pot.OnActivated += PotActivated;
        }
      
        public void Dispose()
        {
            _pot.OnActivated -= PotActivated;
        }
        
        private void PotActivated()
        {
            _gameCycleController.PauseGame();
            _pot.OnActivated -= PotActivated;
            _uiManager.ShowGardeningUi(_pot, OnExitGardeningUi);
        }
        
        private void OnExitGardeningUi()
        {
            _gameCycleController.ResumeGame();
            _pot.OnActivated += PotActivated;
        }
    }
}