using System.Collections.Generic;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter : BasePresenter
    {
        private const string Title = "Готовка";
        private const string Repeat = "Повторить";

        private readonly IPanelView _view;
        private readonly DishCrafter _crafter;
        private readonly PresentersFactory _factory;
        private readonly ActiveDishRecipe _activeRecipe;
        private readonly Transform _canvas;
        private readonly ResourceItemConfig _slopConfig;

        private readonly CookingRecipesPresenter _recipesPresenter;
        private readonly CookingAndMatchRecipePresenter _cookingAndMatchRecipePresenter;
        private readonly CookingIngredientsPresenter _ingredientsPresenter;
        
        private InfoPresenter _infoPresenter;
        private DishRecipe _dishRecipe;
        private List<ProductItem> _productItems;
        private List<LootItem> _lootItems;

        public CookingPanelPresenter(
            IPanelView view, 
            DishCrafter crafter,
            PresentersFactory factory,
            ActiveDishRecipe activeRecipe,
            Transform canvas,
            ResourceItemConfig slopConfig
            ) : base(view)
        {
            _view = view;
            _crafter = crafter;
            _factory = factory;
            _activeRecipe = activeRecipe;
            _canvas = canvas;
            _slopConfig = slopConfig;
            _recipesPresenter = factory.CreateLeftGridPresenter(_view.Container);
            _cookingAndMatchRecipePresenter = factory.CreateCookingAndMatchRecipePresenter(
                _view.Container, activeRecipe);
            _ingredientsPresenter = factory.CreateCookingIngredientsPresenter(_view.Container, activeRecipe);
        }

        protected override void OnShow()
        {
            _activeRecipe.Reset();
            SetupView();
            SetupLeftPanel();
            SetupMiniGame();
            SetupIngredients();

            _crafter.OnDishCrafted += OnDishCrafted;
            _crafter.OnSlopCrafted += OnSlopCrafted;
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _recipesPresenter.Hide();
            _recipesPresenter.OnMatchNewRecipe -= Reset;
            _recipesPresenter.OnSetRecipe -= SetRecipe;
            
            _cookingAndMatchRecipePresenter.Hide();
            
            _ingredientsPresenter.Hide();
            _ingredientsPresenter.OnTryAddItem -= OnTryAddItemToActiveRecipe;
            
            _activeRecipe.Reset();
            
            _crafter.OnDishCrafted -= OnDishCrafted;
            _crafter.OnSlopCrafted -= OnSlopCrafted;
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
        }

        private void SetupLeftPanel()
        {
            _recipesPresenter.OnMatchNewRecipe += Reset;
            _recipesPresenter.OnSetRecipe += SetRecipe;
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

        private void SetRecipe(DishRecipe recipe) => _activeRecipe.Setup(recipe);

        private void OnDishCrafted(DishRecipe recipe, DishItem dishItem) => ShowInfo(recipe, dishItem);

        private void OnSlopCrafted(List<ProductItem> productItems, List<LootItem> lootItems)
        {
            ShowInfo(productItems, lootItems, _slopConfig.GetItem());
        }

        private void ShowInfo(DishRecipe recipe, Item item)
        {
            _infoPresenter ??= _factory.CreateInfoPresenter(_canvas);

            if (!_infoPresenter.Show(item, Repeat)) return;
            
            _dishRecipe = recipe;
            
            _infoPresenter.OnAccepted += RepeatDish;
            _infoPresenter.OnRejected += OnCancelled;
        }

        private void ShowInfo(List<ProductItem> productItems, List<LootItem> lootItems, Item item)
        {
            _infoPresenter ??= _factory.CreateInfoPresenter(_canvas);

            if (!_infoPresenter.Show(item, Repeat)) return;
            
            _productItems = productItems;
            _lootItems = lootItems;
            
            _infoPresenter.OnAccepted += RepeatIngredients;
            _infoPresenter.OnRejected += OnCancelledIngredients;
        }

        private void RepeatDish(Item _)
        {
            UnsubscribeInfoDish();

            _activeRecipe.Setup(_dishRecipe);
            _dishRecipe = null;
        }

        private void OnCancelled()
        {
            UnsubscribeInfoDish();
        }

        private void RepeatIngredients(Item _)
        {
            UnsubscribeInfoIngredients();
            
            foreach (ProductItem item in _productItems)
            {
                _activeRecipe.AddProduct(item);
            }

            foreach (LootItem item in _lootItems)
            {
                _activeRecipe.AddLoot(item);
            }

            _productItems = null;
            _lootItems = null;
            
        }

        private void OnCancelledIngredients()
        {
            UnsubscribeInfoIngredients();
        }

        private void UnsubscribeInfoDish()
        {
            _infoPresenter.OnAccepted -= RepeatDish;
            _infoPresenter.OnRejected -= OnCancelled;
        }

        private void UnsubscribeInfoIngredients()
        {
            _infoPresenter.OnAccepted -= RepeatIngredients;
            _infoPresenter.OnRejected -= OnCancelledIngredients;
        }
    }
}