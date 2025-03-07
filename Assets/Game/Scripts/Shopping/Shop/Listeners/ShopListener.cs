using Modules.GameCycle;
using Tavern.UI;

namespace Tavern.Shopping
{
    public class ShopListener
    {
        private readonly ShopContext _shopContext;
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _uiManager;

        public ShopListener(
            ShopContext shopContext, 
            GameCycle gameCycle,
            IUiManager uiManager
            )
        {
            _shopContext = shopContext;
            _gameCycle = gameCycle;
            _uiManager = uiManager;

            _shopContext.OnActivated += ShopContextActivated;
        }

        public void Dispose()
        {
            _shopContext.OnActivated -= ShopContextActivated;
        }
        
        private void ShopContextActivated()
        {
            _gameCycle.PauseGame();
            _shopContext.OnActivated -= ShopContextActivated;
            _uiManager.ShowShoppingUi(_shopContext.Shop, OnExitShoppingUi);
        }
        
        private void OnExitShoppingUi()
        {
            _gameCycle.ResumeGame();
            _shopContext.OnActivated += ShopContextActivated;
        }
    }
}