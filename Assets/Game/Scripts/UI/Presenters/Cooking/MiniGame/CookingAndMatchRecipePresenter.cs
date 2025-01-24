using System;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Settings;

namespace Tavern.UI.Presenters
{
    public class CookingAndMatchRecipePresenter : BasePresenter
    {
        public event Action<Item> OnReturnItem;
        
        private readonly CookingUISettings _settings;
        private readonly CookingMiniGamePresenter _cookingMiniGamePresenter;
        private readonly RecipeIngredientsPresenter _recipeIngredientsPresenter;
        private readonly RecipeEffectsPresenter _recipeEffectsPresenter;

        public CookingAndMatchRecipePresenter(
            ICookingAndMatchRecipeView view,
            PresentersFactory factory,
            ActiveDishRecipe recipe
            ) : base(view)
        {
            _cookingMiniGamePresenter = factory.CreateCookingMiniGamePresenter(view.Transform);
            _recipeIngredientsPresenter = factory.CreateRecipeIngredientsPresenter(view.Transform, recipe);
            _recipeEffectsPresenter = factory.CreateRecipeEffectsPresenter(view.Transform);
        }

        protected override void OnShow()
        {
            _cookingMiniGamePresenter.Show();
            
            _recipeIngredientsPresenter.Show();
            _recipeIngredientsPresenter.OnReturnItem += OnReturn;
            
            _recipeEffectsPresenter.Show();
        }

        protected override void OnHide()
        {
            _cookingMiniGamePresenter.Hide();
            
            _recipeIngredientsPresenter.Hide();
            _recipeIngredientsPresenter.OnReturnItem -= OnReturn;
            
            _recipeEffectsPresenter.Hide();
        }

        private void OnReturn(Item item) => OnReturnItem?.Invoke(item);
    }
}