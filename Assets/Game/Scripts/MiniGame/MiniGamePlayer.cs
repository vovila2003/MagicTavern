using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tavern.Cooking;
using Tavern.MiniGame.UI;
using UnityEngine;
using VContainer;

namespace Tavern.MiniGame
{
    public class MiniGamePlayer : MonoBehaviour, IDisposable
    {
        
        private readonly Dictionary<DishRecipe, int> _recipes = new();

        private DishRecipe _currentRecipe;
        private DishCookbookContext _cookbook;
        private IMiniGamePresenter _presenter;
        private MiniGameConfig _config;

        [Inject]
        public void Construct(
            IMiniGamePresenter presenter,
            DishCookbookContext cookbook,
            MiniGameConfig config)
        {
            _cookbook = cookbook;
            _presenter = presenter;
            _config = config;
            
            _presenter.OnRestart += RestartGame;
            _presenter.OnGameOver += GameOver;
            Debug.Log("Inject");
        }

        void IDisposable.Dispose()
        {
            _presenter.OnRestart -= RestartGame;
            _presenter.OnGameOver -= GameOver;
        }

        [Button]
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