using System.Collections.Generic;
using Tavern.Cooking;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class RecipeEffectsPresenter : BasePresenter
    {
        private readonly IRecipeEffectsView _view;
        private readonly CookingUISettings _settings;
        private readonly IActiveDishRecipeReader _recipe;
        private readonly RecipeMatcher _matcher;

        public RecipeEffectsPresenter(
            IRecipeEffectsView view,
            CookingUISettings settings,
            IActiveDishRecipeReader recipe,
            RecipeMatcher matcher) : base(view)
        {
            _view = view;
            _settings = settings;
            _recipe = recipe;
            _matcher = matcher;
        }
        
        protected override void OnShow()
        {
            ResetEffects();
            _matcher.OnRecipeMatched += OnRecipeMatched;
            
        }

        protected override void OnHide()
        {
            _matcher.OnRecipeMatched -= OnRecipeMatched;
        }

        private void ResetEffects()
        {
            foreach (IEffectView effectView in _view.RecipeEffects)
            {
                effectView.SetIcon(_settings.DefaultSprite);
            }
        }

        private void OnRecipeMatched(bool state)
        {
            ResetEffects();

            if (!state) return;
            
            SetEffects();
        }

        private void SetEffects()
        {
            List<IEffectComponent> effects = _recipe.Recipe.ResultItem.Item.GetAll<IEffectComponent>();
            
            int count = Mathf.Min(effects.Count, _view.RecipeEffects.Count);
            for (var i = 0; i < count; i++)
            {
                IEffectView viewEffect = _view.RecipeEffects[i];
                viewEffect.SetIcon(effects[i].Config.Icon);
            }
        }
    }
}