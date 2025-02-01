using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IView
    {
        RectTransform RectTransform { get; }
        void Show();
        void Hide();
    }
}