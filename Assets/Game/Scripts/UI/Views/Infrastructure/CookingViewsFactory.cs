using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class CookingViewsFactory : ICookingViewsFactory
    {
        private readonly UISettings _settings;

        public CookingViewsFactory(UISettings settings)
        {
            _settings = settings;
        }

        public IMatchNewRecipeView CreateMatchNewRecipeView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.MatchNewNewRecipeView, viewContainer);

        public IContainerView CreateCookingIngredientsView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.CookingIngredientsView, viewContainer);

        public ICookingAndMatchRecipeView CreateCookingAndMatchRecipeView(Transform viewContainer) =>
            Object.Instantiate(_settings.CookingSettings.CookingAndMatchRecipeView, viewContainer);

        public ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.CookingMiniGameView, viewContainer);

        public IRecipeIngredientsView CreateRecipeIngredientsView(Transform viewContainer) =>
            Object.Instantiate(_settings.CookingSettings.RecipeIngredientsView, viewContainer);
        
        public IRecipeEffectsView CreateRecipeEffectsView(Transform viewContainer) =>
            Object.Instantiate(_settings.CookingSettings.RecipeEffectsView, viewContainer);
    }
}