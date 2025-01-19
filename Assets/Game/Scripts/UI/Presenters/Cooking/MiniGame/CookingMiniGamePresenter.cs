using Tavern.Settings;
using Tavern.UI.Views;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        private const string ComponentName = "Название компонента";
        private readonly ICookingMiniGameView _view;
        private readonly CookingUISettings _settings;

        public CookingMiniGamePresenter(ICookingMiniGameView view, CookingUISettings settings) : base(view)
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
            ResetIngredients();
            ResetEffects();

            //TODO clear recipe matcher
        }

        private void ResetIngredients()
        {
            foreach (IngredientView ingredientView in _view.RecipeIngredients)
            {
                ingredientView.SetTitle(ComponentName);
                ingredientView.SetIcon(_settings.DefaultSprite);
                ingredientView.SetBackgroundColor(_settings.EmptyColor);
            }
        }

        private void ResetEffects()
        {
            foreach (RecipeEffectView effectView in _view.RecipeEffects)
            {
                effectView.SetIcon(_settings.DefaultSprite);
            }
        }
    }
}