using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Tavern.Cooking;
using Tavern.MiniGame.UI;
using UnityEngine;

namespace Tavern.MiniGame
{
    [UsedImplicitly]
    public class MiniGamePlayer : IDisposable
    {
        private readonly Dictionary<DishRecipe, int> _recipes = new();
        private readonly DishCookbookContext _cookbook;
        private readonly IMiniGamePresenter _presenter;
        private readonly MiniGameConfig _config;
        private DishRecipe _currentRecipe;

        public MiniGamePlayer(
            IMiniGamePresenter presenter,
            DishCookbookContext cookbook,
            MiniGameConfig config)
        {
            _cookbook = cookbook;
            _presenter = presenter;
            _config = config;
            
            _presenter.OnRestart += RestartGame;
            _presenter.OnGameOver += GameOver;
        }

        public void CreateGame(DishRecipe recipe)
        {
            if (recipe is null) return;

            if (_cookbook.HasRecipe(recipe))
            {
                Debug.Log($"Recipe {recipe.Name} is already in cookbook");
                return;
            }
            
            int currentScore = _recipes.GetValueOrDefault(recipe, 0);
            
            _presenter.Show(currentScore, _config.Match);
            _currentRecipe = recipe;
        }

        void IDisposable.Dispose()
        {
            _presenter.OnRestart -= RestartGame;
            _presenter.OnGameOver -= GameOver;
        }

        private void GameOver(bool win)
        {
            if (_currentRecipe is null) return;

            if (!win) return;
            
            _recipes.TryAdd(_currentRecipe, 0);
            _recipes[_currentRecipe]++;

            if (_recipes[_currentRecipe] < _config.Match) return;
            
            _cookbook.AddRecipe(_currentRecipe);
            _recipes.Remove(_currentRecipe);
        }

        private void RestartGame() => CreateGame(_currentRecipe);
    }
}