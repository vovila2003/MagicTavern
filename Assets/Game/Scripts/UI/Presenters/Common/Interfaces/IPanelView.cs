using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IPanelView : IView
    {
        Transform Container { get; }
        event UnityAction OnCloseClicked;
        void SetTitle(string title);
    }
}