using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ICommonViewsFactory
    {
        IEntityCardViewPool EntityCardViewPool { get; }
        IItemCardViewPool ItemCardViewPool { get; }
        IInfoViewProvider InfoViewProvider { get; }
        IEntityCardView GetEntityCardView(Transform viewContentTransform);
        IItemCardView GetItemCardView(Transform viewContentTransform);
        IPanelView CreatePanelView();
        IContainerView CreateLeftGridView(Transform viewContainer);
    }
}