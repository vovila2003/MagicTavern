using System;
using Modules.Items;
using Tavern.Settings;

namespace Tavern.UI.Presenters
{
    public class CookingAndMatchRecipePresenter : BasePresenter
    {
        public event Action<Item> OnReturnItem;
        
        private readonly CookingUISettings _settings;
        private readonly MiniGamePresenter _miniGamePresenter;
        private readonly RecipeIngredientsPresenter _recipeIngredientsPresenter;

        public CookingAndMatchRecipePresenter(
            ICookingMiniGameView view, 
            PresentersFactory factory) : base(view)
        {
            _miniGamePresenter = factory.;
            _recipeIngredientsPresenter = factory.;
        }

        protected override void OnShow()
        {
            _miniGamePresenter.Show();
            _recipeIngredientsPresenter.Show();
        }

        protected override void OnHide()
        {
            _miniGamePresenter.Hide();
            _recipeIngredientsPresenter.Hide();
        }

        public void MatchNewRecipe() => _recipeIngredientsPresenter.MatchNewRecipe();

        public bool TryAddIngredient(Item item) => _recipeIngredientsPresenter.TryAddIngredient(item);
    }
}