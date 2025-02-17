using Tavern.Infrastructure;
using Tavern.UI;

namespace Tavern.Shopping
{
    public class ShopListener
    {
        private readonly Shop _shop;
        private readonly GameCycleController _gameCycleController;
        private readonly UiManager _uiManager;

        public ShopListener(
            Shop shop, 
            GameCycleController gameCycleController,
            UiManager uiManager
            )
        {
            _shop = shop;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;

            _shop.OnActivated += OnShopActivated;
        }

        public void Dispose()
        {
            _shop.OnActivated -= OnShopActivated;
        }
        
        private void OnShopActivated()
        {
            _gameCycleController.PauseGame();
            _shop.OnActivated -= OnShopActivated;
            _uiManager.ShowShoppingUi(_shop.SellerConfig, OnExitShoppingUi);
        }
        
        private void OnExitShoppingUi()
        {
            _gameCycleController.ResumeGame();
            _shop.OnActivated += OnShopActivated;
        }
    }
}