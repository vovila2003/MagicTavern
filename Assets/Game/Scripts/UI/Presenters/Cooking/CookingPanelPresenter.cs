using System;
using System.Collections.Generic;
using Modules.Items;
using Tavern.Cooking;
using Tavern.ProductsAndIngredients;
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
        private readonly CommonPresentersFactory _commonFactory;
        private readonly ActiveDishRecipe _activeRecipe;
        private readonly Transform _canvas;
        private readonly SlopsItem _slopsItem;

        private readonly CookingRecipesPresenter _recipesPresenter;
        private readonly CookingAndMatchRecipePresenter _cookingAndMatchRecipePresenter;
        private readonly CookingIngredientsPresenter _ingredientsPresenter;
        
        private InfoPresenter _infoPresenter;
        private DishRecipe _dishRecipe;
        private List<PlantProductItem> _plantProductItems;
        private List<AnimalProductItem> _animalProductItems;
        private KitchenItemConfig _kitchenItemConfig;
        private Action _onExit;

        public CookingPanelPresenter(
            IPanelView view, 
            DishCrafter crafter,
            CookingPresentersFactory factory,
            CommonPresentersFactory commonFactory,
            ActiveDishRecipe activeRecipe,
            Transform canvas,
            SlopsItemConfig slopConfig
            ) : base(view)
        {
            _view = view;
            _crafter = crafter;
            _commonFactory = commonFactory;
            _activeRecipe = activeRecipe;
            _canvas = canvas;
            _slopsItem = slopConfig.Create() as SlopsItem;
            _recipesPresenter = factory.CreateLeftGridPresenter(_view.Container);
            _cookingAndMatchRecipePresenter = factory.CreateCookingAndMatchRecipePresenter(_view.Container);
            _ingredientsPresenter = factory.CreateCookingIngredientsPresenter(_view.Container, activeRecipe);
        }
        
        public void Show(KitchenItemConfig kitchenItemConfig, Action onExit)
        {
            _kitchenItemConfig = kitchenItemConfig;
            _onExit = onExit;
            Show();
        }

        protected override void OnShow()
        {
            _activeRecipe.Reset();
            SetupView();
            SetupLeftPanel();
            SetupMiniGame(_kitchenItemConfig.Metadata.Icon);
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
            
            _onExit?.Invoke();
        }

        private void SetupView()
        {
            _view.SetTitle($"{Title}: {_kitchenItemConfig.Metadata.Description}");
            _view.OnCloseClicked += Hide;
        }

        private void SetupLeftPanel()
        {
            _recipesPresenter.OnMatchNewRecipe += Reset;
            _recipesPresenter.OnSetRecipe += SetRecipe;
            _recipesPresenter.Show();
        }

        private void SetupMiniGame(Sprite sprite)
        {
            _cookingAndMatchRecipePresenter.Show(sprite);            
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
                case PlantProductItem productItem:
                    _activeRecipe.AddPlantProduct(productItem);
                    break;
                case AnimalProductItem animalProductItem:
                    _activeRecipe.AddAnimalProduct(animalProductItem);
                    break;
            }
        }

        private void SetRecipe(DishRecipe recipe) => _activeRecipe.Setup(recipe);

        private void OnDishCrafted(DishRecipe recipe, DishItem dishItem) => ShowInfo(recipe, dishItem);

        private void OnSlopCrafted(List<PlantProductItem> plantProductItems, List<AnimalProductItem> animalProductItems)
        {
            ShowInfo(plantProductItems, animalProductItems, _slopsItem);
        }

        private void ShowInfo(DishRecipe recipe, Item item)
        {
            _infoPresenter ??= _commonFactory.CreateInfoPresenter(_canvas);

            if (!_infoPresenter.Show(item, Repeat)) return;
            
            _dishRecipe = recipe;
            
            _infoPresenter.OnAccepted += RepeatDish;
            _infoPresenter.OnRejected += OnCancelled;
        }

        private void ShowInfo(
            List<PlantProductItem> plantProductItems, 
            List<AnimalProductItem> animalProductItems, 
            Item item)
        {
            _infoPresenter ??= _commonFactory.CreateInfoPresenter(_canvas);

            if (!_infoPresenter.Show(item, Repeat)) return;
            
            _plantProductItems = plantProductItems;
            _animalProductItems = animalProductItems;
            
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
            
            foreach (PlantProductItem item in _plantProductItems)
            {
                _activeRecipe.AddPlantProduct(item);
            }

            foreach (AnimalProductItem item in _animalProductItems)
            {
                _activeRecipe.AddAnimalProduct(item);
            }

            _plantProductItems = null;
            _animalProductItems = null;
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