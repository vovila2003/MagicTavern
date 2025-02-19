using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ICookingViewsFactory
    {
        ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer);
        IContainerView CreateCookingIngredientsView(Transform viewContainer);
        IMatchNewRecipeView CreateMatchNewRecipeView(Transform viewContainer);
        ICookingAndMatchRecipeView CreateCookingAndMatchRecipeView(Transform viewContainer);
        IRecipeIngredientsView CreateRecipeIngredientsView(Transform viewContainer);
        IRecipeEffectsView CreateRecipeEffectsView(Transform viewContainer);
    }
}