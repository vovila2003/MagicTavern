using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;

namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter : BasePresenter
    {
        private const string Title = "Готовка";
        
        private readonly IPanelView _view;
        private readonly PresentersFactory _presentersFactory;
        
        private LeftGridRecipesPresenter _leftGridRecipesPresenter;
        private CookingMiniGamePresenter _cookingMiniGamePresenter;
        private CookingIngredientsPresenter _cookingIngredientsPresenter;

        public CookingPanelPresenter(IPanelView view, PresentersFactory presentersFactory) : base(view)
        {
            _view = view;
            _presentersFactory = presentersFactory;
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
            
            _cookingMiniGamePresenter.Hide();
            _cookingMiniGamePresenter.OnReturnItem -= OnReturnItem;
            
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
            _leftGridRecipesPresenter ??= _presentersFactory.CreateLeftGridPresenter(_view.Container);
            _leftGridRecipesPresenter.OnMatchRecipe += OnMatchRecipe;
            _leftGridRecipesPresenter.Show();
        }

        private void SetupMiniGame()
        {
            _cookingMiniGamePresenter ??= _presentersFactory.CreateCookingMiniGamePresenter(_view.Container);
            _cookingMiniGamePresenter.OnReturnItem += OnReturnItem;
            _cookingMiniGamePresenter.Show();            
        }

        private void SetupIngredients()
        {
            _cookingIngredientsPresenter ??= _presentersFactory.CreateCookingIngredientsPresenter(_view.Container);
            _cookingIngredientsPresenter.OnTryAddItem += OnTryAddItemToRecipeIngredients;
            _cookingIngredientsPresenter.Show();
        }

        private void OnMatchRecipe()
        {
            _cookingMiniGamePresenter.MatchNewRecipe();
        }

        private void OnTryAddItemToRecipeIngredients(Item item)
        {
            if (!_cookingMiniGamePresenter.TryAddIngredient(item)) return;

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