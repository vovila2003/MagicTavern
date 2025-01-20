using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IViewsFactory
    {
        IEntityCardViewPool EntityCardViewPool { get; }
        IItemCardViewPool ItemCardViewPool { get; }
        IEntityCardView GetEntityCardView(Transform viewContentTransform);
        IItemCardView GetItemCardView(Transform viewContentTransform);
        IPanelView CreatePanelView();
        IContainerView CreateLeftGridView(Transform viewContainer);
        ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer);
        IContainerView CreateCookingIngredientsView(Transform viewContainer);
        IMatchRecipeView CreateMatchRecipeView(Transform viewContainer);
        IInfoPanelView CreateInfoPanelView();
    }
}