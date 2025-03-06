using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IConvertInfoView
    {
        event UnityAction OnAction;
        event UnityAction OnClose;
        event UnityAction OnMax;
        event UnityAction OnMin;
        event UnityAction<float> OnSliderChanged;
        event Action<int> OnPlus;
        event Action<int> OnMinus;
        void SetTitle(string title);
        void SetDescription(string title);
        void SetIcon(Sprite icon);
        void SetAmount(string amount);
        void SetSliderMaxValue(float value);
        void SetSliderValue(float value);
        void SetRatio(string text);
    }
}