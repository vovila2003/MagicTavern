using System.Collections.Generic;
using Modules.Crafting;
using Tavern.Cooking;

namespace Tavern.UI.Presenters
{
    public class LeftGridRecipesPresenter
    {
        private readonly IContainerView _view;
        private readonly PresentersFactory _presentersFactory;
        private readonly DishCookbookContext _cookbook;
        private readonly Dictionary<DishRecipe, RecipeCardPresenter> _recipeCardPresenters = new();
        private bool _isShown;

        public LeftGridRecipesPresenter(
            IContainerView view, 
            DishCookbookContext cookbook,
            PresentersFactory presentersFactory)
        {
            _view = view;
            _presentersFactory = presentersFactory;
            _cookbook = cookbook;
            _isShown = false;
        }
        
        public void Show()
        {
            if (_isShown) return;
            
            SetupCards();
            
            _cookbook.OnRecipeAdded += OnRecipeAdded;
            _cookbook.OnRecipeRemoved += OnRecipeRemoved;
            _view.Show();
            _isShown = true;
        }

        public void Hide()
        {
            if (!_isShown) return;
            
            _cookbook.OnRecipeAdded -= OnRecipeAdded;
            _cookbook.OnRecipeRemoved -= OnRecipeRemoved;

            _view.Hide();
            
            foreach (RecipeCardPresenter cardPresenter in _recipeCardPresenters.Values)
            {
                cardPresenter.Hide();
            }
            
            _recipeCardPresenters.Clear();
            _isShown = false;
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
            RecipeCardPresenter recipePresenter = _presentersFactory.CreateRecipeCardPresenter(_view.ContentTransform);
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