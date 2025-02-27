using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IEntityCardView : IView
    {
        event UnityAction OnCardClicked;
        Transform Transform { get; }
        void SetTitle(string title);
        void SetIcon(Sprite icon);
        void SetTime(string time);
        void SetMaxStars(int count);
        void SetStars(float stars);
        void SetParent(Transform parent);
        void SetSelected(bool selected);
    }
}