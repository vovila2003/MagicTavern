using Tavern.Infrastructure;
using Tavern.UI;

namespace Tavern.Shopping
{
    public class ShopListener
    {
        private readonly ShopContext _shopContext;
        private readonly GameCycleController _gameCycleController;
        private readonly UiManager _uiManager;

        public ShopListener(
            ShopContext shopContext, 
            GameCycleController gameCycleController,
            UiManager uiManager
            )
        {
            _shopContext = shopContext;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;

            _shopContext.OnActivated += ShopContextActivated;
        }

        public void Dispose()
        {
            _shopContext.OnActivated -= ShopContextActivated;
        }
        
        private void ShopContextActivated()
        {
            _gameCycleController.PauseGame();
            _shopContext.OnActivated -= ShopContextActivated;
            _uiManager.ShowShoppingUi(_shopContext.Shop, OnExitShoppingUi);
        }
        
        private void OnExitShoppingUi()
        {
            _gameCycleController.ResumeGame();
            _shopContext.OnActivated += ShopContextActivated;
        }
    }
}