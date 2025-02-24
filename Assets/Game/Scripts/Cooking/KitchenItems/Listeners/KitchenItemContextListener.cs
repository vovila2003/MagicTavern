using Tavern.Infrastructure;
using Tavern.UI;

namespace Tavern.Cooking
{
    public class KitchenItemContextListener
    {
        private readonly KitchenItemContext _kitchenItemContext;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly GameCycleController _gameCycleController;
        private readonly IUiManager _uiManager;

        public KitchenItemContextListener(
            KitchenItemContext kitchenItemContext, 
            ActiveDishRecipe activeDishRecipe,
            GameCycleController gameCycleController,
            IUiManager uiManager
            )
        {
            _kitchenItemContext = kitchenItemContext;
            _activeDishRecipe = activeDishRecipe;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;

            _kitchenItemContext.OnActivated += OnKitchenActivated;
        }

        public void Dispose()
        {
            _kitchenItemContext.OnActivated -= OnKitchenActivated;
        }

        private void OnKitchenActivated(KitchenItemConfig config)
        {
            _activeDishRecipe.SetKitchen(config);
            _gameCycleController.PauseGame();
            _kitchenItemContext.OnActivated -= OnKitchenActivated;
            _uiManager.ShowCookingUi(config, OnExitCookingUi);
        }

        private void OnExitCookingUi()
        {
            _gameCycleController.ResumeGame();
            _kitchenItemContext.OnActivated += OnKitchenActivated;
        }
    }
}