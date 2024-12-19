using System;

namespace Tavern.MiniGame.UI
{
    public interface IMiniGameView
    {
        event Action OnStartGame;
        event Action OnCloseGame;
        event Action OnRestartGame;
        void Show();
        void Hide();
        void SetSliderValue(float value);
        void SetTarget(float start, float end);
        void ShowStartButton();
        void HideStartButton();
        void ShowCloseButton();
        void HideCloseButton();
        void ShowRestartButton();
        void HideRestartButton();
        void SetResultText(string text);
        void SetScoreText(string text);
    }
}