using System;
using System.Collections.Generic;
using Modules.Info;
using Modules.Items;
using Tavern.Cooking;
using Tavern.ProductsAndIngredients;
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
        private readonly Func<Transform, InfoPresenter> _infoPresenterFactory;
        private readonly Transform _canvas;
        private readonly ActiveDishRecipe _recipe;
        private readonly bool _enableMatching;
        private readonly Dictionary<IRecipeIngredientView, Item> _views = new();
        
        private InfoPresenter _infoPresenter;

        public RecipeIngredientsPresenter(
            IRecipeIngredientsView view, 
            CookingUISettings settings,
            Func<Transform, InfoPresenter> infoPresenterFactory, 
            Transform canvas,
            ActiveDishRecipe recipe,
            bool enableMatching
            ) : base(view)
        {
            _view = view;
            _settings = settings;
            _infoPresenterFactory = infoPresenterFactory;
            _canvas = canvas;
            _recipe = recipe;
            _enableMatching = enableMatching;
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
            SetupIngredients(ref offset, _recipe.PlantProducts, isFake: false);
            SetupIngredients(ref offset, _recipe.AnimalProducts, isFake: false);
            SetupIngredients(ref offset, _recipe.FakePlantProducts, isFake: true);
            SetupIngredients(ref offset, _recipe.FakeAnimalProducts, isFake: true);
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
                Metadata metadata = item.Metadata;
                recipeIngredientView.SetTitle(metadata.Title);
                recipeIngredientView.SetIcon(metadata.Icon);
                recipeIngredientView.SetBackgroundColor(_settings.FilledColor);
                recipeIngredientView.SetFake(isFake);

                if (_enableMatching)
                {
                    recipeIngredientView.OnLeftClicked += OnIngredientLeftClicked;
                    recipeIngredientView.OnRightClicked += OnIngredientRightClicked;
                }
                
                _views.Add(recipeIngredientView, item);
                i++;
            }
            
            offset += count;
        }

        private void OnIngredientLeftClicked(IRecipeIngredientView view)
        {
            Item item = _views[view];
            _infoPresenter ??= _infoPresenterFactory(_canvas);
            
            if (!_infoPresenter.Show(item, InfoPresenter.Mode.Dialog, Return)) return;
            
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
                case PlantProductItem plantProductItem:
                    _recipe.RemovePlantProduct(plantProductItem);
                    break;
                case AnimalProductItem animalProductItem:
                    _recipe.RemoveAnimalProduct(animalProductItem);
                    break;
            }
        }

        private void OnCancelled() => UnsubscribeInfo();

        private void OnIngredientRightClicked(IRecipeIngredientView view) => ReturnItem(_views[view]);

        private void UnsubscribeIngredientView(IRecipeIngredientView view)
        {
            if (!_enableMatching) return;
            
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