using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Tavern.MiniGame.UI
{
    [UsedImplicitly]
    public class MiniGamePresenter : IDisposable, IMiniGamePresenter
    {
        public event Action OnRestart;
        public event Action<bool> OnGameOver;
        
        private readonly IMiniGameView _view;

        private readonly MiniGame _game;
        private int _currentScore;
        private int _maxScore;

        public MiniGamePresenter(IMiniGameView view, MiniGame game)
        {
            _view = view;
            _game = game;
            
            _view.OnStartGame += OnStartGame;
            _view.OnCloseGame += OnCloseGame;
            _view.OnRestartGame += OnRestartGame;
            
            _game.OnTargetChanged += OnTargetChanged;
            _game.OnValueChanged += OnValueChanged;
            _game.OnResult += OnResult;
        }

        public void Show(int currentScore, int maxScore)
        {

            _currentScore = currentScore;
            _maxScore = maxScore;
            
            _view.SetResultText("");
            _view.SetScoreText($"{currentScore} / {maxScore}");
            _view.SetSliderValue(0);
            _view.ShowStartButton();
            _view.HideCloseButton();
            _view.HideRestartButton();
            _view.Show();
        }

        void IDisposable.Dispose()
        {
            _view.OnStartGame -= OnStartGame;
            _view.OnCloseGame -= OnCloseGame;
            _view.OnRestartGame -= OnRestartGame;
            
            _game.OnTargetChanged -= OnTargetChanged;
            _game.OnValueChanged -= OnValueChanged;
            _game.OnResult -= OnResult;
        }

        private void OnStartGame()
        {
            _view.HideStartButton();
            _game.StartGame();
        }

        private void OnRestartGame() => OnRestart?.Invoke();

        private void OnTargetChanged(Vector2 value) => _view.SetTarget(value.x, value.y);

        private void OnValueChanged(float value) => _view.SetSliderValue(value);

        private void OnResult(bool result)
        {
            if (result)
            {
                _currentScore++;
                _view.SetScoreText($"{_currentScore} / {_maxScore}");
            }

            if (_currentScore < _maxScore)
            {
                _view.ShowRestartButton();
            }
            
            _view.SetResultText(GetResultText(result));
            _view.ShowCloseButton();
            
            OnGameOver?.Invoke(result);
        }

        private string GetResultText(bool result)
        {
            string resultText = result? "YOU WIN!" : "You lose!";
            if (_currentScore >= _maxScore)
            {
                resultText += "\nRecipe record completed!";
            }
            
            return resultText;
        }

        private void OnCloseGame()
        {
            _view.Hide();
        }
    }
}