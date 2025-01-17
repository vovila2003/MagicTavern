using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IViewsFactory
    {
        IEntityCardViewPool EntityCardViewPool { get; }
        IEntityCardView GetEntityCardView();
        IPanelView CreatePanelView();
        ILeftGridView CreateLeftGridView(Transform viewContainer);
        ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer);
        IIngredientsView CreateCookingIngredientsView(Transform viewContainer);
    }
}