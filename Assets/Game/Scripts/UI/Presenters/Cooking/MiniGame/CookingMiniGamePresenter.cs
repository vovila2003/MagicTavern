using System;
using System.Collections.Generic;
using Modules.Items;
using Tavern.Settings;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        public event Action<Item> OnReturnItem;
        
        private const string ComponentName = "Название компонента";
        private readonly ICookingMiniGameView _view;
        private readonly CookingUISettings _settings;
        private readonly List<Item> _ingredients = new();
        private readonly Dictionary<IngredientView, Item> _views = new();

        public CookingMiniGamePresenter(ICookingMiniGameView view, CookingUISettings settings) : base(view)
        {
            _view = view;
            _settings = settings;
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        public void MatchNewRecipe()
        {
            ResetIngredients();
            ResetEffects();
            ReturnAllItems();
            
            //TODO clear recipe matcher
        }

        public bool TryAddIngredient(Item item)
        {
            if (_ingredients.Count >= _view.RecipeIngredients.Length) return false;
            
            _ingredients.Add(item);
            RepaintIngredients();

            return true;
        }

        private void RepaintIngredients()
        {
            ResetIngredients();
            SetupIngredients();
        }

        private void SetupIngredients()
        {
            for (int i = 0; i < _ingredients.Count; i++)
            {
                Item item = _ingredients[i];
                IngredientView ingredientView = _view.RecipeIngredients[i];
                SetupIngredientView(item, ingredientView);
                _views.Add(ingredientView, item);
            }
        }

        private void ResetIngredients()
        {
            foreach (IngredientView ingredientView in _view.RecipeIngredients)
            {
                ingredientView.SetTitle(ComponentName);
                ingredientView.SetIcon(_settings.DefaultSprite);
                ingredientView.SetBackgroundColor(_settings.EmptyColor);
                Unsubscribe(ingredientView);
            }
            
            _views.Clear();
        }

        private void ResetEffects()
        {
            foreach (RecipeEffectView effectView in _view.RecipeEffects)
            {
                effectView.SetIcon(_settings.DefaultSprite);
            }
        }

        private void ReturnAllItems()
        {
            foreach (Item item in _ingredients)
            {
                OnReturnItem?.Invoke(item);        
            }
            
            _ingredients.Clear();
        }

        private void OnIngredientLeftClicked(IngredientView view)
        {
            //TODO
            Item item = _views[view];
            Debug.Log($"Item {item.ItemName} clicked by left button");
        }

        private void OnIngredientRightClicked(IngredientView view)
        {
            Item item = _views[view];
            _ingredients.Remove(item);
            Unsubscribe(view);
            OnReturnItem?.Invoke(item);
            RepaintIngredients();
        }

        private void SetupIngredientView(Item item, IngredientView ingredientView)
        {
            ItemMetadata metadata = item.ItemMetadata;
            ingredientView.SetTitle(metadata.Title);
            ingredientView.SetIcon(metadata.Icon);
            ingredientView.SetBackgroundColor(_settings.FilledColor);
            Subscribe(ingredientView);
        }

        private void Subscribe(IngredientView view)
        {
            view.OnLeftClicked += OnIngredientLeftClicked;
            view.OnRightClicked += OnIngredientRightClicked;
        }

        private void Unsubscribe(IngredientView view)
        {
            view.OnLeftClicked -= OnIngredientLeftClicked;
            view.OnRightClicked -= OnIngredientRightClicked;
        }
    }
}