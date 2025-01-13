using System.Collections.Generic;
using Modules.Crafting;
using Tavern.Cooking;

namespace Tavern.UI
{
    public class LeftGridPresenter
    {
        private readonly DishCookbookContext _cookbook;
        private readonly PresentersFactory _presentersFactory;
        
        private readonly LeftGridView _view;
        
        private readonly Dictionary<DishRecipe, RecipeCardPresenter> _recipeCardPresenters = new();

        public LeftGridPresenter(LeftGridView view, PresentersFactory presentersFactory, DishCookbookContext  cookbook)
        {
            _view = view;
            _presentersFactory = presentersFactory;
            _cookbook = cookbook;
        }

        public void Show()
        {
            SetupCards();

            _cookbook.OnRecipeAdded += OnRecipeAdded;
            _cookbook.OnRecipeRemoved += OnRecipeRemoved;
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            foreach (RecipeCardPresenter cardPresenter in _recipeCardPresenters.Values)
            {
                cardPresenter.Hide();
            }
            
            _cookbook.OnRecipeAdded -= OnRecipeAdded;
            _cookbook.OnRecipeRemoved -= OnRecipeRemoved;
            _view.gameObject.SetActive(false);
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
            RecipeCardPresenter recipePresenter = _presentersFactory.CreateRecipeCardPresenter();
            _recipeCardPresenters.Add(dishRecipe, recipePresenter);
            _view.AddCardView(recipePresenter.View);
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