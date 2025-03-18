using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IConvertInfoViewProvider
    {
        bool TryGetView(Transform parent, out IConvertInfoView view);
        bool TryRelease(IConvertInfoView _);
    }
}