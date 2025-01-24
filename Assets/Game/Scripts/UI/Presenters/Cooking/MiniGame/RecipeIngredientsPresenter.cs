using System;
using System.Collections.Generic;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Looting;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class RecipeIngredientsPresenter : BasePresenter
    {
        private const string Return = "Убрать";
        private const string ComponentName = "Название компонента";

        private readonly IRecipeIngredientsView _view;
        private readonly CookingUISettings _settings;
        private readonly Func<Transform, ItemInfoPresenter> _infoPresenterFactory;
        private readonly Transform _canvas;
        private readonly ActiveDishRecipe _recipe;
        private readonly Dictionary<IRecipeIngredientView, Item> _views = new();
        
        private ItemInfoPresenter _infoPresenter;

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
            ResetIngredients();
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

        private void SetupIngredients(ref int offset, IReadOnlyCollection<Item> collection, bool isFake)
        {
            int count = collection.Count;
            int i = 0;
            foreach (Item item in collection)
            {
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
                i++;
            }
            
            offset += count;
        }

        private void OnIngredientLeftClicked(IRecipeIngredientView view)
        {
            Item item = _views[view];
            _infoPresenter ??= _infoPresenterFactory(_canvas);
            
            if (!_infoPresenter.Show(item, Return)) return;
            
            _infoPresenter.OnAccepted += Returned;
            _infoPresenter.OnRejected += OnCancelled;
        }

        private void Returned(Item item)
        {
            UnsubscribeInfo();
            ReturnItem(item);
        }

        private void ReturnItem(Item item)
        {
            switch (item)
            {
                case ProductItem productItem:
                    _recipe.RemoveProduct(productItem);
                    break;
                case LootItem lootItem:
                    _recipe.RemoveLoot(lootItem);
                    break;
            }
        }

        private void OnCancelled() => UnsubscribeInfo();

        private void OnIngredientRightClicked(IRecipeIngredientView view) => ReturnItem(_views[view]);

        private void UnsubscribeIngredientView(IRecipeIngredientView view)
        {
            view.OnLeftClicked -= OnIngredientLeftClicked;
            view.OnRightClicked -= OnIngredientRightClicked;
        }

        private void UnsubscribeInfo()
        {
            _infoPresenter.OnAccepted -= Returned;
            _infoPresenter.OnRejected -= OnCancelled;
        }
    }
}