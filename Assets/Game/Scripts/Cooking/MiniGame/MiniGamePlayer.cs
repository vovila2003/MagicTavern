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
        private readonly DishAutoCookbookContext _autoCookbook;
        private readonly MiniGame _game;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly DishCrafter _dishCrafter;
        private bool _canPlay;

        public MiniGamePlayer(
            DishCookbookContext cookbook, 
            DishAutoCookbookContext autoCookbook, 
            MiniGame game,
            ActiveDishRecipe activeDishRecipe,
            DishCrafter dishCrafter,
            ISpaceInput inputService)
        {
            _cookbook = cookbook;
            _autoCookbook = autoCookbook;
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
            int newScore = Mathf.Clamp(stars + result, 0, maxScore);
            bool isExtra = stars == maxScore && newScore == maxScore;
            
            _cookbook.SetRecipeStars(recipe, newScore);

            if (newScore >= maxScore && !_autoCookbook.HasRecipe(recipe))
            {
                _autoCookbook.AddRecipe(recipe);
            }

            if (newScore > 0)
            {
                _dishCrafter.CraftDish(isExtra);
            }
            else
            {
                _dishCrafter.MakeSlops();
            }    
        }

        private void OnValueChanged(float value) => OnGameValueChanged?.Invoke(value);

        private void Unsubscribe() => _game.OnValueChanged -= OnValueChanged;
    }
}