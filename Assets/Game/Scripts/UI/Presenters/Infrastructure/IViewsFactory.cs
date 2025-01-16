using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IViewsFactory
    {
        ILeftGridView CreateLeftGridView(Transform viewContainer);
        IEntityCardViewPool EntityCardViewPool { get; }
        IEntityCardView GetEntityCardView();
        ICookingView CreateCookingPanelView();
    }
}