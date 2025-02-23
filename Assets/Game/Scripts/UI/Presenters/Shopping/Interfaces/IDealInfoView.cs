using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IDealInfoView
    {
        event UnityAction OnAction;
        event UnityAction OnClose;
        event UnityAction OnPlus1;
        event UnityAction OnPlus3;
        event UnityAction OnPlus5;
        event UnityAction OnPlus10;
        event UnityAction OnMax;
        event UnityAction OnMinus1;
        event UnityAction OnMinus3;
        event UnityAction OnMinus5;
        event UnityAction OnMinus10;
        event UnityAction OnMin;
        event UnityAction<float> OnSliderChanged;
        IEffectView[] Effects { get; }
        void SetTitle(string title);
        void SetDescription(string title);
        void SetIcon(Sprite icon);
        void HideAllEffects();
        void SetExtra(bool isExtra);
        void SetAmount(string amount);
        void SetSliderMAxValue(float value);
        void SetSliderValue(float value);
    }
}