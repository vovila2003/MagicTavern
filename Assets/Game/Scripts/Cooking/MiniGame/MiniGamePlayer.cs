using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.Cooking.MiniGame
{
    [UsedImplicitly]
    public class MiniGamePlayer : IInitGameListener, IExitGameListener
    {
        public event Action OnGameStarted;
        public event Action OnGameStopped;
        public event Action<float> OnGameValueChanged;
        public event Action<bool> OnGameAvailableChange;
        
        private readonly DishCookbookContext _cookbook;
        private readonly MiniGame _game;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly DishCrafter _dishCrafter;
        private bool _canPlay;

        public MiniGamePlayer(
            DishCookbookContext cookbook, 
            MiniGame game,
            ActiveDishRecipe activeDishRecipe,
            DishCrafter dishCrafter,
            ISpaceInput inputService)
        {
            _cookbook = cookbook;
            _game = game;
            _activeDishRecipe = activeDishRecipe;
            _dishCrafter = dishCrafter;
        }

        public Regions GetRegions()
        {
            return _game.CreateGame(_activeDishRecipe.Recipe.GameConfig);
        }

        public void Activate()
        {
            if (!_canPlay) return;
            
            if (_game.IsPlaying)
            {
                Stop();
                return;
            }

            Start();
        }

        public int StopGame()
        {
            int result = _game.StopGame();
            Unsubscribe();
            OnGameStopped?.Invoke();
            
            return result;
        }

        void IInitGameListener.OnInit()
        {
            _dishCrafter.OnStateChanged += OnCrafterStateChanged;
        }

        void IExitGameListener.OnExit()
        {
            _dishCrafter.OnStateChanged -= OnCrafterStateChanged;
        }

        private void Start()
        {
            _game.StartGame();
            _game.OnValueChanged += OnValueChanged;
            OnGameStarted?.Invoke();
        }

        private void OnCrafterStateChanged(bool state)
        {
            _canPlay = state;
            StopGame();
            OnGameAvailableChange?.Invoke(state);
        }

        private void Stop()
        {
            int result = StopGame();
            ProcessGameResult(result);
        }

        private void ProcessGameResult(int result)
        {
            DishRecipe recipe = _activeDishRecipe.Recipe;
            int stars = _cookbook.GetRecipeStars(recipe);
            int maxScore = recipe.StarsCount * 2;
            int score = Mathf.Clamp(stars + result, 0, maxScore);
            _cookbook.SetRecipeStars(recipe, score);
            
            //TODO
        }

        private void OnValueChanged(float value) => OnGameValueChanged?.Invoke(value);

        private void Unsubscribe()
        {
            _game.OnValueChanged -= OnValueChanged;
        }

        private void GameOver(int value)
        {
            // if (_currentRecipe is null) return;
            //
            // _recipes.TryAdd(_currentRecipe, 0);
            // int maxScore = _currentRecipe.StarsCount * 2;
            // int score = Mathf.Clamp(_recipes[_currentRecipe] + value, 0, maxScore);
            // _recipes[_currentRecipe] = score;
            //
            // if (_recipes[_currentRecipe] < maxScore) return;
            //
            // _cookbook.AddRecipe(_currentRecipe);
            // _recipes.Remove(_currentRecipe);
            // Debug.Log($"Recipe {_currentRecipe.Name} added to cookbook");
        }
    }
}