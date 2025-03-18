using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class CookingViewsFactory : ICookingViewsFactory
    {
        private readonly UISettings _uiSettings;

        public CookingViewsFactory(GameSettings settings)
        {
            _uiSettings = settings.UISettings;
        }

        public IMatchNewRecipeView CreateMatchNewRecipeView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.CookingSettings.MatchNewNewRecipeView, viewContainer);

        public IContainerView CreateCookingIngredientsView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.CookingSettings.CookingIngredientsView, viewContainer);

        public ICookingAndMatchRecipeView CreateCookingAndMatchRecipeView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.CookingSettings.CookingAndMatchRecipeView, viewContainer);

        public ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.CookingSettings.CookingMiniGameView, viewContainer);

        public IRecipeIngredientsView CreateRecipeIngredientsView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.CookingSettings.RecipeIngredientsView, viewContainer);
        
        public IRecipeEffectsView CreateRecipeEffectsView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.CookingSettings.RecipeEffectsView, viewContainer);
    }
}