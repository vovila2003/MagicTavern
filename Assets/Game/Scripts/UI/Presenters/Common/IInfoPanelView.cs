using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IInfoPanelView
    {
        event UnityAction OnAction;
        event UnityAction OnClose;
        void SetTitle(string title);
        void SetDescription(string title);
        void SetIcon(Sprite icon);
        void SetActionButtonText(string text);
    }
}