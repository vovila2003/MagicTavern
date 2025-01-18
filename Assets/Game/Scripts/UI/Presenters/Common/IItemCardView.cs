using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IItemCardView : IView
    {
        event UnityAction OnCardClicked;
        void SetIcon(Sprite icon);
        void SetParent(Transform parent);
        void SetCount(string count);
    }
}