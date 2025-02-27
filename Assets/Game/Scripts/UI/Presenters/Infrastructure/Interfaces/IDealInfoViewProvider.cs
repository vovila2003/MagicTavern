using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IDealInfoViewProvider
    {
        bool TryGetView(Transform parent, out IDealInfoView view);
        bool TryRelease(IDealInfoView _);
    }
}