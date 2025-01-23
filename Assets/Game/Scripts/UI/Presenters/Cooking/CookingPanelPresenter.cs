using Modules.Items;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Looting;

namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter : BasePresenter
    {
        private const string Title = "Готовка";
        
        private readonly IPanelView _view;
        private readonly ActiveDishRecipe _activeRecipe;
        private readonly CookingRecipesPresenter _cookingRecipesPresenter;
        private readonly CookingAndMatchRecipePresenter _cookingAndMatchRecipePresenter;
        private readonly CookingIngredientsPresenter _cookingIngredientsPresenter;

        public CookingPanelPresenter(
            IPanelView view, 
            PresentersFactory presentersFactory,
            ActiveDishRecipe activeRecipe
            ) : base(view)
        {
            _view = view;
            _activeRecipe = activeRecipe;
            _cookingRecipesPresenter = presentersFactory.CreateLeftGridPresenter(_view.Container);
            _cookingAndMatchRecipePresenter = presentersFactory.CreateCookingAndMatchRecipePresenter(_view.Container);
            _cookingIngredientsPresenter = presentersFactory.CreateCookingIngredientsPresenter(_view.Container);
        }

        protected override void OnShow()
        {
            SetupView();
            SetupLeftPanel();
            SetupMiniGame();
            SetupIngredients();
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _cookingRecipesPresenter.Hide();
            _cookingRecipesPresenter.OnMatchNewRecipe -= Reset;
            _cookingRecipesPresenter.OnTryPrepareRecipe -= TryPrepareRecipe;
            
            _cookingAndMatchRecipePresenter.Hide();
            _cookingAndMatchRecipePresenter.OnReturnItem -= OnReturnItem;
            
            _cookingIngredientsPresenter.Hide();
            _cookingIngredientsPresenter.OnTryAddItem -= OnTryAddItemToRecipeIngredients;
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
        }

        private void SetupLeftPanel()
        {
            _cookingRecipesPresenter.OnMatchNewRecipe += Reset;
            _cookingRecipesPresenter.OnTryPrepareRecipe += TryPrepareRecipe;
            _cookingRecipesPresenter.Show();
        }

        private void SetupMiniGame()
        {
            _cookingAndMatchRecipePresenter.OnReturnItem += OnReturnItem;
            _cookingAndMatchRecipePresenter.Show();            
        }

        private void SetupIngredients()
        {
            _cookingIngredientsPresenter.OnTryAddItem += OnTryAddItemToRecipeIngredients;
            _cookingIngredientsPresenter.Show();
        }

        private void Reset()
        {
            _cookingAndMatchRecipePresenter.Reset();
        }

        private void OnTryAddItemToRecipeIngredients(Item item)
        {
            if (!_cookingAndMatchRecipePresenter.TryAddIngredient(item)) return;

            switch (item)
            {
                case ProductItem productItem:
                    _cookingIngredientsPresenter.RemoveProduct(productItem);
                    break;
                case LootItem lootItem:
                    _cookingIngredientsPresenter.RemoveLoot(lootItem);
                    break;
            }
        }

        private void OnReturnItem(Item item)
        {
            switch (item)
            {
                case ProductItem productItem:
                    _cookingIngredientsPresenter.AddProduct(productItem);
                    break;
                case LootItem lootItem:
                    _cookingIngredientsPresenter.AddLoot(lootItem);
                    break;
            }
        }

        private void TryPrepareRecipe(DishRecipe recipe)
        {
            Reset();
            _activeRecipe.Setup(recipe);
        }
    }
}