using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IInfoViewProvider
    {
        bool TryGetView(Transform parent, out IInfoPanelView view);
        bool TryRelease(IInfoPanelView _);
    }
}