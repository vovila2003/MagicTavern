using Modules.GameCycle;
using Tavern.UI;

namespace Tavern.Gardening
{
    public class PotListener
    {
        private readonly Pot _pot;
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _uiManager;

        public PotListener(
            Pot pot, 
            GameCycle gameCycle,
            IUiManager uiManager
        )
        {
            _pot = pot;
            _gameCycle = gameCycle;
            _uiManager = uiManager;

            _pot.OnActivated += PotActivated;
        }
      
        public void Dispose()
        {
            _pot.OnActivated -= PotActivated;
        }
        
        private void PotActivated()
        {
            _gameCycle.PauseGame();
            _pot.OnActivated -= PotActivated;
            _uiManager.ShowGardeningUi(_pot, OnExitGardeningUi);
        }
        
        private void OnExitGardeningUi()
        {
            _gameCycle.ResumeGame();
            _pot.OnActivated += PotActivated;
        }
    }
}