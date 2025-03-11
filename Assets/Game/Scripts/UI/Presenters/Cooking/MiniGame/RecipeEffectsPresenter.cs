using System.Collections.Generic;
using Tavern.Cooking;
using Tavern.Effects;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class RecipeEffectsPresenter : BasePresenter
    {
        private readonly IRecipeEffectsView _view;
        private readonly CookingUISettings _settings;
        private readonly IActiveDishRecipeReader _activeRecipe;

        public RecipeEffectsPresenter(
            IRecipeEffectsView view,
            CookingUISettings settings,
            IActiveDishRecipeReader activeRecipe
            ) : base(view)
        {
            _view = view;
            _settings = settings;
            _activeRecipe = activeRecipe;
        }
        
        protected override void OnShow()
        {
            ResetEffects();
            _activeRecipe.OnChanged += OnRecipeChanged;
        }

        protected override void OnHide()
        {
            _activeRecipe.OnChanged -= OnRecipeChanged;
        }

        private void OnRecipeChanged()
        {
            ResetEffects();
            if (_activeRecipe.IsEmpty) return;
            
            SetEffects();
        }

        private void ResetEffects()
        {
            foreach (IEffectView effectView in _view.RecipeEffects)
            {
                effectView.SetIcon(_settings.DefaultSprite);
            }
        }

        private void SetEffects()
        {
            List<IEffectComponent> effects = _activeRecipe.Recipe.ResultItemConfig.GetAllExtra<IEffectComponent>();
            
            int count = Mathf.Min(effects.Count, _view.RecipeEffects.Count);
            for (var i = 0; i < count; i++)
            {
                IEffectView viewEffect = _view.RecipeEffects[i];
                viewEffect.SetIcon(effects[i].Config.Icon);
            }
        }
    }
}