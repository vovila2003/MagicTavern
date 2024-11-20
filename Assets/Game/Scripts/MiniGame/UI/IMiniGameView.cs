using System;

namespace Tavern.MiniGame.UI
{
    public interface IMiniGameView
    {
        event Action OnStartGame;
        event Action OnCloseGame;
        void Show();
        void Hide();
        void SetSliderValue(float value);
        void SetTarget(float start, float end);
        void ShowStartButton();
        void HideStartButton();
        void ShowCloseButton();
        void HideCloseButton();
        void SetResultText(string text);
    }
}