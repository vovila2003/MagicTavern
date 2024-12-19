using System;

namespace Tavern.MiniGame.UI
{
    public interface IMiniGamePresenter
    {
        event Action OnRestart;
        event Action<bool> OnGameOver;
        void Show(int currentScore, int maxScore);
    }
}