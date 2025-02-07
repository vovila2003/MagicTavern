using System;
using System.Collections.Generic;
using Modules.Crafting;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingRecipesPresenter : BasePresenter
    {
        public event Action OnMatchNewRecipe; 
        public event Action<DishRecipe> OnSetRecipe; 
        
        private readonly Transform _parent;
        private readonly IContainerView _view;
        private readonly CookingPresentersFactory _cookingPresentersFactory;
        private readonly ActiveDishRecipe _activeRecipe;
        private readonly DishCookbookContext _cookbook;
        private MatchNewRecipePresenter _matchNewRecipePresenter;
        private readonly Dictionary<DishRecipe, RecipeCardPresenter> _recipeCardPresenters = new();

        public CookingRecipesPresenter(
            IContainerView view, 
            DishCookbookContext cookbook,
            CookingPresentersFactory cookingPresentersFactory,
            ActiveDishRecipe activeRecipe
            ) : base(view)
        {
            _parent = view.ContentTransform;
            _view = view;
            _cookingPresentersFactory = cookingPresentersFactory;
            _activeRecipe = activeRecipe;
            _cookbook = cookbook;
        }

        protected override void OnShow()
        {
            SetupMatchRecipe();
            SetupCards();
            
            _cookbook.OnRecipeAdded += OnRecipeAdded;
            _cookbook.OnRecipeRemoved += OnRecipeRemoved;
            _cookbook.OnStarsChanged += OnRecipeStarsChanged;

            _activeRecipe.OnChanged += OnRecipeChanged;
        }

        protected override void OnHide()
        {
            _cookbook.OnRecipeAdded -= OnRecipeAdded;
            _cookbook.OnRecipeRemoved -= OnRecipeRemoved;
            _cookbook.OnStarsChanged -= OnRecipeStarsChanged;
            
            _activeRecipe.OnChanged -= OnRecipeChanged;
            
            _matchNewRecipePresenter.Hide();
            _matchNewRecipePresenter.OnPressed -= MatchNewRecipePressed;
            
            foreach (RecipeCardPresenter cardPresenter in _recipeCardPresenters.Values)
            {
                cardPresenter.Hide();
                cardPresenter.OnRecipeClicked -= OnRecipeClicked;
            }
            
            _recipeCardPresenters.Clear();
        }

        private void OnRecipeStarsChanged(DishRecipe recipe, int value)
        {
            if (!_recipeCardPresenters.TryGetValue(recipe, out RecipeCardPresenter presenter)) return;
            
            presenter.SetStars(value);
        }

        private void SetupMatchRecipe()
        {
            _matchNewRecipePresenter ??= _cookingPresentersFactory.CreateMatchNewRecipePresenter(_parent);
            _matchNewRecipePresenter.OnPressed += MatchNewRecipePressed;
            _matchNewRecipePresenter.Show();
        }

        private void SetupCards()
        {
            KitchenItemConfig requiredKitchen = _activeRecipe.RequiredKitchen;

            foreach (ItemRecipe<DishItem> recipe in _cookbook.Recipes.Values)
            {
                if (recipe is not DishRecipe dishRecipe) continue;

                if (dishRecipe.KitchenItem != requiredKitchen) continue;

                AddPresenter(dishRecipe);
            }
        }

        private void AddPresenter(DishRecipe dishRecipe)
        {
            RecipeCardPresenter recipePresenter = _cookingPresentersFactory.CreateRecipeCardPresenter(_parent);
            recipePresenter.OnRecipeClicked += OnRecipeClicked;
            _recipeCardPresenters.Add(dishRecipe, recipePresenter);
            recipePresenter.Show(dishRecipe, _cookbook.GetRecipeStars(dishRecipe));
        }

        private void OnRecipeAdded(ItemRecipe<DishItem> recipe)
        {
            if (recipe is not DishRecipe dishRecipe) return;
            
            AddPresenter(dishRecipe);
        }

        private void OnRecipeRemoved(ItemRecipe<DishItem> recipe)
        {
            if (recipe is not DishRecipe dishRecipe) return;

            if (_recipeCardPresenters.Remove(dishRecipe, out RecipeCardPresenter presenter))
            {
                presenter.Hide();
            }
        }

        private void MatchNewRecipePressed()
        {
            SetUnselectedAllPresenters();
            OnMatchNewRecipe?.Invoke();
        }

        private void OnRecipeClicked(DishRecipe recipe)
        {
            SetUnselectedAllPresenters();
            OnSetRecipe?.Invoke(recipe);
        }

        private void OnRecipeChanged()
        {
            SetUnselectedAllPresenters();
            
            if (_activeRecipe.IsEmpty) return;

            DishRecipe recipe = _activeRecipe.Recipe;
            if (!_recipeCardPresenters.TryGetValue(recipe, out RecipeCardPresenter presenter)) return;
            
            presenter.SetSelected(true);
            presenter.Up();
            _view.Up();

        }

        private void SetUnselectedAllPresenters()
        {
            foreach (RecipeCardPresenter presenter in _recipeCardPresenters.Values)
            {
                presenter.SetSelected(false);
            }
        }
    }
}