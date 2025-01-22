using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;

namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter : BasePresenter
    {
        private const string Title = "Готовка";
        
        private readonly IPanelView _view;
        private readonly LeftGridRecipesPresenter _leftGridRecipesPresenter;
        private readonly CookingAndMatchRecipePresenter _cookingAndMatchRecipePresenter;
        private readonly CookingIngredientsPresenter _cookingIngredientsPresenter;

        public CookingPanelPresenter(IPanelView view, PresentersFactory presentersFactory) : base(view)
        {
            _view = view;
            _leftGridRecipesPresenter = presentersFactory.CreateLeftGridPresenter(_view.Container);
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
            
            _leftGridRecipesPresenter.Hide();
            _leftGridRecipesPresenter.OnMatchRecipe -= OnMatchRecipe;
            
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
            _leftGridRecipesPresenter.OnMatchRecipe += OnMatchRecipe;
            _leftGridRecipesPresenter.Show();
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

        private void OnMatchRecipe()
        {
            _cookingAndMatchRecipePresenter.MatchNewRecipe();
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
    }
}