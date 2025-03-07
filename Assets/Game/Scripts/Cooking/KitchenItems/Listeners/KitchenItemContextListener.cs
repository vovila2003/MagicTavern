using Modules.GameCycle;
using Tavern.UI;

namespace Tavern.Cooking
{
    public class KitchenItemContextListener
    {
        private readonly KitchenItemContext _kitchenItemContext;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _uiManager;

        public KitchenItemContextListener(
            KitchenItemContext kitchenItemContext, 
            ActiveDishRecipe activeDishRecipe,
            GameCycle gameCycle,
            IUiManager uiManager
            )
        {
            _kitchenItemContext = kitchenItemContext;
            _activeDishRecipe = activeDishRecipe;
            _gameCycle = gameCycle;
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
            _gameCycle.PauseGame();
            _kitchenItemContext.OnActivated -= OnKitchenActivated;
            _uiManager.ShowCookingUi(config, OnExitCookingUi);
        }

        private void OnExitCookingUi()
        {
            _gameCycle.ResumeGame();
            _kitchenItemContext.OnActivated += OnKitchenActivated;
        }
    }
}