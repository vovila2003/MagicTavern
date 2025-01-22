using Tavern.Settings;

namespace Tavern.UI.Presenters
{
    public class RecipeEffectsPresenter : BasePresenter
    {
        private readonly IRecipeEffectsView _view;
        private readonly CookingUISettings _settings;

        public RecipeEffectsPresenter(
            IRecipeEffectsView view,
            CookingUISettings settings) : base(view)
        {
            _view = view;
            _settings = settings;
        }
        
        protected override void OnShow()
        {
        }
        
        protected override void OnHide()
        {
        }

        public void MatchNewRecipe()
        {
            ResetEffects();
        }
        
        private void ResetEffects()
        {
            foreach (IRecipeEffectView effectView in _view.RecipeEffects)
            {
                effectView.SetIcon(_settings.DefaultSprite);
            }
        }
    }
}