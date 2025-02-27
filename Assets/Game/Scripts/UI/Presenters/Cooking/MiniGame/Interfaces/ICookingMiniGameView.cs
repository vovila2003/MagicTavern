using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface ICookingMiniGameView : IView
    {
        event UnityAction OnButtonClicked;
        void SetIcon(Sprite icon);
        void SetTimerText(string text);
        void SetSliderValue(float value);
        void SetGreenZone(float min, float max);
        void SetYellowZone(float min, float max);
        void SetStartButtonActive(bool value);
        void SetButtonText(string text);
    }
}