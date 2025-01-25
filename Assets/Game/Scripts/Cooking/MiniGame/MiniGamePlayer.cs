using System;
using JetBrains.Annotations;
using Tavern.InputServices.Interfaces;

namespace Tavern.Cooking.MiniGame
{
    [UsedImplicitly]
    public class MiniGamePlayer
    {
        public event Action OnGameStarted;
        public event Action OnGameStopped;
        public event Action<float> OnGameValueChanged;
        
        private readonly DishCookbookContext _cookbook;
        private readonly MiniGame _game;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly ISpaceInput _inputService;

        public MiniGamePlayer(
            DishCookbookContext cookbook, 
            MiniGame game,
            ActiveDishRecipe activeDishRecipe,
            ISpaceInput inputService)
        {
            _cookbook = cookbook;
            _game = game;
            _activeDishRecipe = activeDishRecipe;
            _inputService = inputService;
        }

        public Regions GetRegions()
        {
            return _game.CreateGame(_activeDishRecipe.Recipe.GameConfig);
        }

        public void Activate()
        {
            if (_game.IsPlaying)
            {
                Stop();
                return;
            }

            Start();
        }

        private void Start()
        {
            _game.StartGame();
            _activeDishRecipe.OnChanged += OnRecipeChanged;
            _inputService.OnSpace += Stop;
            _game.OnValueChanged += OnValueChanged;
            OnGameStarted?.Invoke();
        }

        private void OnRecipeChanged()
        {
            StopGame();
        }

        private void Stop()
        {
            int result = StopGame();
            ProcessGameResult(result);
        }

        private int StopGame()
        {
            int result = _game.StopGame();
            Unsubscribe();
            OnGameStopped?.Invoke();
            
            return result;
        }

        private void ProcessGameResult(int result)
        {
            DishRecipe recipe = _activeDishRecipe.Recipe;
            int stars = _cookbook.GetRecipeStars(recipe);
            //TODO
        }

        private void OnValueChanged(float value) => OnGameValueChanged?.Invoke(value);

        private void Unsubscribe()
        {
            _activeDishRecipe.OnChanged -= OnRecipeChanged;
            _game.OnValueChanged -= OnValueChanged;
            _inputService.OnSpace -= Stop;
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