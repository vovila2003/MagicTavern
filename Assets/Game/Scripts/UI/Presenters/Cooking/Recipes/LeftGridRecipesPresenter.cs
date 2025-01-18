using System.Collections.Generic;
using Modules.Crafting;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class LeftGridRecipesPresenter : BasePresenter
    {
        private readonly Transform _parent;
        private readonly PresentersFactory _presentersFactory;
        private readonly DishCookbookContext _cookbook;
        private readonly Dictionary<DishRecipe, RecipeCardPresenter> _recipeCardPresenters = new();

        public LeftGridRecipesPresenter(
            IContainerView view, 
            DishCookbookContext cookbook,
            PresentersFactory presentersFactory) : base(view)
        {
            _parent = view.ContentTransform;
            _presentersFactory = presentersFactory;
            _cookbook = cookbook;
        }

        protected override void OnShow()
        {
            SetupCards();
            
            _cookbook.OnRecipeAdded += OnRecipeAdded;
            _cookbook.OnRecipeRemoved += OnRecipeRemoved;
        }

        protected override void OnHide()
        {
            _cookbook.OnRecipeAdded -= OnRecipeAdded;
            _cookbook.OnRecipeRemoved -= OnRecipeRemoved;

            
            foreach (RecipeCardPresenter cardPresenter in _recipeCardPresenters.Values)
            {
                cardPresenter.Hide();
            }
            
            _recipeCardPresenters.Clear();
        }

        private void SetupCards()
        {
            foreach (ItemRecipe<DishItem> recipe in _cookbook.Recipes.Values)
            {
                if (recipe is not DishRecipe dishRecipe) continue;

                AddPresenter(dishRecipe);
            }
        }

        private void AddPresenter(DishRecipe dishRecipe)
        {
            RecipeCardPresenter recipePresenter = _presentersFactory.CreateRecipeCardPresenter(_parent);
            _recipeCardPresenters.Add(dishRecipe, recipePresenter);
            recipePresenter.Show(dishRecipe);
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
    }
}