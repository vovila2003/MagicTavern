using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Tavern.Cooking.MiniGame.UI
{
    [UsedImplicitly]
    public class MiniGamePresenter : IDisposable, IMiniGamePresenter
    {
        public event Action<int> OnGameOver;

        private readonly IMiniGameView _view;

        private readonly MiniGame _game;
        private DishRecipe _recipe;

        public MiniGamePresenter(IMiniGameView view, MiniGame game)
        {
            _view = view;
            _game = game;
            
            _view.OnStartGame += OnStartGame;
            
            _game.OnRegionsChanged += RegionsChanged;
            _game.OnValueChanged += OnValueChanged;
            _game.OnResult += OnResult;
        }

        public void Show(int currentScore, DishRecipe recipe)
        {
            _recipe = recipe;
            
            UpdateScore(currentScore, recipe.StarsCount);
            _game.CreateGame(recipe.GameConfig);

            _view.SetSliderValue(0);
            _view.ShowStartButton();
            _view.Show();
        }

        public void Hide()
        {
            _recipe = null;
            _view.Hide();
        }

        public void UpdateScore(int score, int maxScore)
        {
            _view.SetScoreText($"{score / 2.0f} / {maxScore}");
        }

        void IDisposable.Dispose()
        {
            _view.OnStartGame -= OnStartGame;
            
            _game.OnRegionsChanged -= RegionsChanged;
            _game.OnValueChanged -= OnValueChanged;
            _game.OnResult -= OnResult;
        }

        private void OnStartGame()
        {
            _view.HideStartButton();
            _game.StartGame();
        }

        private void RegionsChanged(Regions regions) => _view.SetRegions(regions);

        private void OnValueChanged(float value) => _view.SetSliderValue(value);

        private void OnResult(int result)
        {
            _view.ShowStartButton();
            OnGameOver?.Invoke(result);
        }
    }
}