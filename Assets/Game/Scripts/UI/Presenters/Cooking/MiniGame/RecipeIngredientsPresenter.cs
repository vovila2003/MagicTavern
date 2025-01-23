using System;
using System.Collections.Generic;
using Modules.Items;
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
        private readonly List<Item> _ingredients = new();
        private readonly Dictionary<IRecipeIngredientView, Item> _views = new();
        private ItemInfoPresenter _itemInfoPresenter;

        public RecipeIngredientsPresenter(
            IRecipeIngredientsView view, 
            CookingUISettings settings,
            Func<Transform, ItemInfoPresenter> infoPresenterFactory, 
            Transform canvas) : base(view)
        {
            _view = view;
            _settings = settings;
            _infoPresenterFactory = infoPresenterFactory;
            _canvas = canvas;
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        public void Reset()
        {
            ResetIngredients();
            ReturnAllItems();
        }

        public bool TryAddIngredient(Item item)
        {
            if (_ingredients.Count >= _view.RecipeIngredients.Count) return false;
            
            _ingredients.Add(item);
            RepaintIngredients();

            return true;
        }

        private void RepaintIngredients()
        {
            ResetIngredients();
            SetupIngredients();
        }

        private void ResetIngredients()
        {
            foreach (IRecipeIngredientView ingredientView in _view.RecipeIngredients)
            {
                ingredientView.SetTitle(ComponentName);
                ingredientView.SetIcon(_settings.DefaultSprite);
                ingredientView.SetBackgroundColor(_settings.EmptyColor);
                UnsubscribeIngredientView(ingredientView);
            }
            
            _views.Clear();
        }

        private void SetupIngredients()
        {
            for (int i = 0; i < _ingredients.Count; i++)
            {
                Item item = _ingredients[i];
                IRecipeIngredientView recipeIngredientView = _view.RecipeIngredients[i];
                SetupIngredientView(item, recipeIngredientView);
                _views.Add(recipeIngredientView, item);
            }
        }

        private void SetupIngredientView(Item item, IRecipeIngredientView recipeIngredientView)
        {
            ItemMetadata metadata = item.ItemMetadata;
            recipeIngredientView.SetTitle(metadata.Title);
            recipeIngredientView.SetIcon(metadata.Icon);
            recipeIngredientView.SetBackgroundColor(_settings.FilledColor);
            SubscribeIngredientView(recipeIngredientView);
        }

        private void SubscribeIngredientView(IRecipeIngredientView view)
        {
            view.OnLeftClicked += OnIngredientLeftClicked;
            view.OnRightClicked += OnIngredientRightClicked;
        }

        private void ReturnAllItems()
        {
            foreach (Item item in _ingredients)
            {
                OnReturnItem?.Invoke(item);        
            }
            
            _ingredients.Clear();
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
            ReturnItem(item);
        }

        private void OnCancelled() => UnsubscribeItemInfo();

        private void OnIngredientRightClicked(IRecipeIngredientView view) => 
            ReturnItem(_views[view]);

        private void ReturnItem(Item item)
        {
            _ingredients.Remove(item);
            OnReturnItem?.Invoke(item);
            RepaintIngredients();
        }

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