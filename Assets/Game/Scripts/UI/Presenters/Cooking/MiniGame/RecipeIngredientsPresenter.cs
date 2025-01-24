using System;
using System.Collections.Generic;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class RecipeIngredientsPresenter : BasePresenter
    {
        private const string Return = "Убрать";
        private const string ComponentName = "Название компонента";
        public event Action<Item> OnReturnItem;
        
        private readonly IRecipeIngredientsView _view;
        private readonly CookingUISettings _settings;
        private readonly Func<Transform, ItemInfoPresenter> _infoPresenterFactory;
        private readonly Transform _canvas;
        private readonly ActiveDishRecipe _recipe;
        private readonly Dictionary<IRecipeIngredientView, Item> _views = new();
        
        private ItemInfoPresenter _itemInfoPresenter;

        public RecipeIngredientsPresenter(
            IRecipeIngredientsView view, 
            CookingUISettings settings,
            Func<Transform, ItemInfoPresenter> infoPresenterFactory, 
            Transform canvas,
            ActiveDishRecipe recipe) : base(view)
        {
            _view = view;
            _settings = settings;
            _infoPresenterFactory = infoPresenterFactory;
            _canvas = canvas;
            _recipe = recipe;
        }

        protected override void OnShow()
        {
            _recipe.OnChanged += SetupRecipe;
        }

        protected override void OnHide()
        {
            _recipe.OnChanged -= SetupRecipe;
        }

        private void SetupRecipe()
        {
            ResetIngredients();
            
            int offset = 0;
            SetupIngredients(ref offset, _recipe.Products, false);
            SetupIngredients(ref offset, _recipe.Loots, false);
            SetupIngredients(ref offset, _recipe.FakeProducts, true);
            SetupIngredients(ref offset, _recipe.FakeLoots, true);
        }

        private void ResetIngredients()
        {
            foreach (IRecipeIngredientView ingredientView in _view.RecipeIngredients)
            {
                ingredientView.SetTitle(ComponentName);
                ingredientView.SetIcon(_settings.DefaultSprite);
                ingredientView.SetBackgroundColor(_settings.EmptyColor);
                ingredientView.SetFake(false);
                UnsubscribeIngredientView(ingredientView);
            }
            
            _views.Clear();
        }

        private void SetupIngredients(ref int offset, IReadOnlyList<Item> collection, bool isFake)
        {
            int count = collection.Count;
            for (int i = 0; i < count; ++i)
            {
                Item item = collection[i];
                if (i + offset >= _view.RecipeIngredients.Count) break;
                
                IRecipeIngredientView recipeIngredientView = _view.RecipeIngredients[i + offset];
                ItemMetadata metadata = item.ItemMetadata;
                recipeIngredientView.SetTitle(metadata.Title);
                recipeIngredientView.SetIcon(metadata.Icon);
                recipeIngredientView.SetBackgroundColor(_settings.FilledColor);
                recipeIngredientView.SetFake(isFake);
                recipeIngredientView.OnLeftClicked += OnIngredientLeftClicked;
                recipeIngredientView.OnRightClicked += OnIngredientRightClicked;
                _views.Add(recipeIngredientView, item);
            }
            
            offset += count;
        }

        private void OnIngredientLeftClicked(IRecipeIngredientView view)
        {
            Item item = _views[view];
            _itemInfoPresenter ??= _infoPresenterFactory(_canvas);
            
            if (!_itemInfoPresenter.Show(item, Return)) return;
            
            _itemInfoPresenter.OnAccepted += OnItemReturned;
            _itemInfoPresenter.OnRejected += OnCancelled;
        }

        private void OnItemReturned(Item item)
        {
            UnsubscribeItemInfo();
            
            OnReturnItem?.Invoke(item);
        }

        private void OnCancelled() => UnsubscribeItemInfo();

        private void OnIngredientRightClicked(IRecipeIngredientView view) => 
            OnReturnItem?.Invoke(_views[view]);

        private void UnsubscribeIngredientView(IRecipeIngredientView view)
        {
            view.OnLeftClicked -= OnIngredientLeftClicked;
            view.OnRightClicked -= OnIngredientRightClicked;
        }

        private void UnsubscribeItemInfo()
        {
            _itemInfoPresenter.OnAccepted -= OnItemReturned;
            _itemInfoPresenter.OnRejected -= OnCancelled;
        }
    }
}