using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingAndMatchRecipePresenter : BasePresenter
    {
        private readonly CookingUISettings _settings;
        private readonly CookingMiniGamePresenter _cookingMiniGamePresenter;
        private readonly RecipeIngredientsPresenter _recipeIngredientsPresenter;
        private readonly RecipeEffectsPresenter _recipeEffectsPresenter;
        private Sprite _sprite;

        public CookingAndMatchRecipePresenter(
            ICookingAndMatchRecipeView view,
            CookingPresentersFactory factory
            ) : base(view)
        {
            _cookingMiniGamePresenter = factory.CreateCookingMiniGamePresenter(view.Transform);
            _recipeIngredientsPresenter = factory.CreateRecipeIngredientsPresenter(view.Transform);
            _recipeEffectsPresenter = factory.CreateRecipeEffectsPresenter(view.Transform);
        }
        
        public void Show(Sprite sprite)
        {
            _sprite = sprite;    
            Show();
        }

        protected override void OnShow()
        {
            _cookingMiniGamePresenter.Show(_sprite);
            
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