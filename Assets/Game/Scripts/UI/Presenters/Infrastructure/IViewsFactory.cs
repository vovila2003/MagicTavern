using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IViewsFactory
    {
        IEntityCardViewPool EntityCardViewPool { get; }
        IItemCardViewPool ItemCardViewPool { get; }
        IInfoViewProvider InfoViewProvider { get; }
        IEntityCardView GetEntityCardView(Transform viewContentTransform);
        IItemCardView GetItemCardView(Transform viewContentTransform);
        IPanelView CreatePanelView();
        IContainerView CreateLeftGridView(Transform viewContainer);
        ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer);
        IContainerView CreateCookingIngredientsView(Transform viewContainer);
        IMatchNewRecipeView CreateMatchNewRecipeView(Transform viewContainer);
        ICookingAndMatchRecipeView CreateCookingAndMatchRecipeView(Transform viewContainer);
        IRecipeIngredientsView CreateRecipeIngredientsView(Transform viewContainer);
        IRecipeEffectsView CreateRecipeEffectsView(Transform viewContainer);
    }
}