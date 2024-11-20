using UnityEngine;

namespace Tavern.MiniGame.UI
{
    public class MiniGamePresenter
    {
        private readonly IMiniGameView _view;

        private readonly MiniGame _game;

        public MiniGamePresenter(IMiniGameView view, MiniGame game)
        {
            _view = view;
            _game = game;
            _view.OnStartGame += OnStartGame;
            _view.OnCloseGame += OnCloseGame;
            _game.OnTargetChanged += OnTargetChanged;
            _game.OnValueChanged += OnValueChanged;
            _game.OnResult += OnResult;
        }

        public void Show()
        {
            _view.SetResultText("");
            _view.SetSliderValue(0);
            _view.ShowStartButton();
            _view.HideCloseButton();
            _view.Show();
        }

        public void Dispose()
        {
            _view.OnStartGame -= OnStartGame;
            _view.OnCloseGame -= OnCloseGame;
            _game.OnTargetChanged -= OnTargetChanged;
            _game.OnValueChanged -= OnValueChanged;
            _game.OnResult -= OnResult;
        }

        private void OnStartGame()
        {
            _view.HideStartButton();
            _game.StartGame();
        }

        private void OnTargetChanged(Vector2 value) => _view.SetTarget(value.x, value.y);

        private void OnValueChanged(float value) => _view.SetSliderValue(value);

        private void OnResult(bool result)
        {
            _view.ShowCloseButton();
            _view.SetResultText(result? "YOU WIN!" : "You lose!");
        }

        private void OnCloseGame()
        {
            _view.Hide();
        }
    }
}