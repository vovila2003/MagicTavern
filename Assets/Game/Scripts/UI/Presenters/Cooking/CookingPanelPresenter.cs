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
        
        private readonly CookingRecipesPresenter _recipesPresenter;
        private readonly CookingAndMatchRecipePresenter _cookingAndMatchRecipePresenter;
        private readonly CookingIngredientsPresenter _ingredientsPresenter;

        public CookingPanelPresenter(
            IPanelView view, 
            PresentersFactory factory,
            ActiveDishRecipe activeRecipe
            ) : base(view)
        {
            _view = view;
            _activeRecipe = activeRecipe;
            _recipesPresenter = factory.CreateLeftGridPresenter(_view.Container);
            _cookingAndMatchRecipePresenter = factory.CreateCookingAndMatchRecipePresenter(
                _view.Container, activeRecipe);
            _ingredientsPresenter = factory.CreateCookingIngredientsPresenter(_view.Container);
        }

        protected override void OnShow()
        {
            _activeRecipe.Reset();
            SetupView();
            SetupLeftPanel();
            SetupMiniGame();
            SetupIngredients();
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _recipesPresenter.Hide();
            _recipesPresenter.OnMatchNewRecipe -= Reset;
            _recipesPresenter.OnTryPrepareRecipe -= TryPrepareRecipe;
            
            _cookingAndMatchRecipePresenter.Hide();
            
            _ingredientsPresenter.Hide();
            _ingredientsPresenter.OnTryAddItem -= OnTryAddItemToActiveRecipe;
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
        }

        private void SetupLeftPanel()
        {
            _recipesPresenter.OnMatchNewRecipe += Reset;
            _recipesPresenter.OnTryPrepareRecipe += TryPrepareRecipe;
            _recipesPresenter.Show();
        }

        private void SetupMiniGame()
        {
            _cookingAndMatchRecipePresenter.Show();            
        }

        private void SetupIngredients()
        {
            _ingredientsPresenter.OnTryAddItem += OnTryAddItemToActiveRecipe;
            _ingredientsPresenter.Show();
        }

        private void Reset() => _activeRecipe.Reset();

        private void OnTryAddItemToActiveRecipe(Item item)
        {
            switch (item)
            {
                case ProductItem productItem:
                    _activeRecipe.AddProduct(productItem);
                    break;
                case LootItem lootItem:
                    _activeRecipe.AddLoot(lootItem);
                    break;
            }
        }

        private void TryPrepareRecipe(DishRecipe recipe) => _activeRecipe.Setup(recipe);
    }
}