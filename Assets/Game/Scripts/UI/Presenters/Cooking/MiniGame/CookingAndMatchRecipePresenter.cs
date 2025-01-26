using Tavern.Cooking;
using Tavern.Settings;

namespace Tavern.UI.Presenters
{
    public class CookingAndMatchRecipePresenter : BasePresenter
    {
        private readonly CookingUISettings _settings;
        private readonly CookingMiniGamePresenter _cookingMiniGamePresenter;
        private readonly RecipeIngredientsPresenter _recipeIngredientsPresenter;
        private readonly RecipeEffectsPresenter _recipeEffectsPresenter;

        public CookingAndMatchRecipePresenter(
            ICookingAndMatchRecipeView view,
            PresentersFactory factory
            ) : base(view)
        {
            _cookingMiniGamePresenter = factory.CreateCookingMiniGamePresenter(view.Transform);
            _recipeIngredientsPresenter = factory.CreateRecipeIngredientsPresenter(view.Transform);
            _recipeEffectsPresenter = factory.CreateRecipeEffectsPresenter(view.Transform);
        }

        protected override void OnShow()
        {
            _cookingMiniGamePresenter.Show();
            
            _recipeIngredientsPresenter.Show();
            
            _recipeEffectsPresenter.Show();
        }

        protected override void OnHide()
        {
            _cookingMiniGamePresenter.Hide();
            
            _recipeIngredientsPresenter.Hide();
            
            _recipeEffectsPresenter.Hide();
        }
    }
}