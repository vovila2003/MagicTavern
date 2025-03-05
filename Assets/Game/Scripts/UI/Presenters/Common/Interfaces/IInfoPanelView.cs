using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IInfoPanelView : IView
    {
        event UnityAction OnAction;
        event UnityAction OnClose;
        IEffectView[] Effects { get; }
        void SetTitle(string title);
        void SetDescription(string title);
        void SetIcon(Sprite icon);
        void SetActionButtonText(string text);
        void HideAllEffects();
        void SetExtra(bool isExtra);
        void SetMode(InfoPresenter.Mode mode);
    }
}