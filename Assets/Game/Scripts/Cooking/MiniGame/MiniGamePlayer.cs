using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Tavern.Cooking.MiniGame.UI;
using UnityEngine;

namespace Tavern.Cooking.MiniGame
{
    // [UsedImplicitly]
    // public class MiniGamePlayer : IDisposable
    // {
    //     private readonly Dictionary<DishRecipe, int> _recipes = new();
    //     private readonly DishCookbookContext _cookbook;
    //     private readonly IMiniGamePresenter _presenter;
    //     private DishRecipe _currentRecipe;
    //
    //     public MiniGamePlayer(
    //         IMiniGamePresenter presenter,
    //         DishCookbookContext cookbook)
    //     {
    //         _cookbook = cookbook;
    //         _presenter = presenter;
    //         
    //         _presenter.OnGameOver += GameOver;
    //     }
    //
    //     public void CreateGame(DishRecipe recipe)
    //     {
    //         if (recipe is null) return;
    //
    //         if (_cookbook.HasRecipe(recipe))
    //         {
    //             Debug.Log($"Recipe {recipe.Name} is already in cookbook");
    //             return;
    //         }
    //         
    //         int currentScore = _recipes.GetValueOrDefault(recipe, 0);
    //         
    //         _currentRecipe = recipe;
    //         _presenter.Show(currentScore, _currentRecipe);
    //     }
    //
    //     void IDisposable.Dispose()
    //     {
    //         _presenter.OnGameOver -= GameOver;
    //     }
    //
    //     private void GameOver(int value)
    //     {
    //         if (_currentRecipe is null) return;
    //
    //         _recipes.TryAdd(_currentRecipe, 0);
    //         int maxScore = _currentRecipe.StarsCount * 2;
    //         int score = Mathf.Clamp(_recipes[_currentRecipe] + value, 0, maxScore);
    //         _recipes[_currentRecipe] = score;
    //         
    //         _presenter.UpdateScore(score, _currentRecipe.StarsCount);
    //
    //         if (_recipes[_currentRecipe] < maxScore) return;
    //         
    //         _cookbook.AddRecipe(_currentRecipe);
    //         _recipes.Remove(_currentRecipe);
    //         _presenter.Hide();
    //         Debug.Log($"Recipe {_currentRecipe.Name} added to cookbook");
    //     }
    // }
}