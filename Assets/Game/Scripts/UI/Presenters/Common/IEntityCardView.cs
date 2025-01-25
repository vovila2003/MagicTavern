using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IEntityCardView : IView
    {
        event UnityAction OnCardClicked;
        void SetTitle(string title);
        void SetIcon(Sprite icon);
        void SetTime(string time);
        void SetMaxStart(int count);
        void SetStars(float stars);
        void SetParent(Transform parent);
    }
}