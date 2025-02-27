using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IDealInfoView : IView
    {
        event UnityAction OnAction;
        event UnityAction OnClose;
        event UnityAction OnMax;
        event UnityAction OnMin;
        event Action<int> OnPlus;
        event Action<int> OnMinus;
        event UnityAction<float> OnSliderChanged;
        IEffectView[] Effects { get; }
        void SetTitle(string title);
        void SetDescription(string title);
        void SetIcon(Sprite icon);
        void HideAllEffects();
        void SetExtra(bool isExtra);
        void SetAmount(string amount);
        void SetSliderMaxValue(float value);
        void SetSliderValue(float value);
        void SetPrice(string text);
        void SetTotalPrice(string text);
    }
}