using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tavern.Cooking.MiniGame
{
    [UsedImplicitly]
    public class MiniGamePlayer : IInitGameListener, IExitGameListener
    {
        public event Action OnGameStarted;
        public event Action OnGameStopped;
        public event Action<float> OnGameValueChanged;
        public event Action<bool> OnGameAvailableChange;

        public event Action<float> OnTimeChanged;
        
        private readonly DishCookbookContext _cookbook;
        private readonly DishAutoCookbookContext _autoCookbook;
        private readonly MiniGame _game;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly DishCrafter _dishCrafter;
        private readonly RecipeMatcher _matcher;
        private readonly MiniGameConfig _defaultConfig;
        private readonly int _minDefaultTime;
        private readonly int _maxDefaultTime;
        private bool _canPlay;
        
        public MiniGamePlayer(
            DishCookbookContext cookbook, 
            DishAutoCookbookContext autoCookbook, 
            MiniGame game,
            ActiveDishRecipe activeDishRecipe,
            DishCrafter dishCrafter,
            RecipeMatcher matcher,
            CookingSettings settings)
        {
            _cookbook = cookbook;
            _autoCookbook = autoCookbook;
            _game = game;
            _activeDishRecipe = activeDishRecipe;
            _dishCrafter = dishCrafter;
            _matcher = matcher;
            _defaultConfig = settings.DefaultMiniGameConfig;
            _minDefaultTime = settings.MinDefaultTime;
            _maxDefaultTime = settings.MaxDefaultTime;
        }

        public GameParams GetGameParams()
        {
            DishRecipe recipe = _activeDishRecipe.Recipe;
            
            if (_activeDishRecipe.IsEmpty)
            {
                if (!_matcher.MatchRecipe(_activeDishRecipe, out DishRecipe matchedRecipe))
                {
                    int time = Random.Range(_minDefaultTime, _maxDefaultTime);
                    return new GameParams
                    {
                        Regions = _game.CreateGame(_defaultConfig, time),
                        TimeInSeconds = time
                    };
                }
                
                recipe = matchedRecipe;
            }

            return new GameParams
            {
                Regions = _game.CreateGame(recipe.GameConfig, recipe.CraftingTimeInSeconds),
                TimeInSeconds = recipe.CraftingTimeInSeconds
            };
        }

        public void Activate()
        {
            if (!_canPlay) return;
            
            if (_game.IsPlaying)
            {
                Stop();
                return;
            }

            if (_activeDishRecipe.IsEmpty)
            {
                bool isExists = _matcher.MatchRecipe(_activeDishRecipe, out DishRecipe recipe);
                if (isExists && _cookbook.HasRecipe(recipe))
                {
                    _activeDishRecipe.Setup(recipe);
                }
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
            _activeDishRecipe.OnCanCraftStateChanged += OnCanCraftStateChanged;
        }

        void IExitGameListener.OnExit()
        {
            _activeDishRecipe.OnCanCraftStateChanged -= OnCanCraftStateChanged;
        }

        private void Start()
        {
            _game.OnValueChanged += OnValueChanged;
            _game.OnTimeChanged += OnGameTimeChanged;
            _game.OnTimeUp += OnTimeUp;
            _game.StartGame();
            OnGameStarted?.Invoke();
        }

        private void OnCanCraftStateChanged(bool state)
        {
            _canPlay = state;
            OnGameAvailableChange?.Invoke(state);
        }

        private void Stop()
        {
            int result = StopGame();
            ProcessGameResult(result);
        }

        private void ProcessGameResult(int result)
        {
            if (_activeDishRecipe.IsEmpty)
            {
                ProcessNewRecipe(result);
                return;
            }
            
            DishRecipe recipe = _activeDishRecipe.Recipe;
            int stars = _cookbook.GetRecipeStars(recipe);
            int maxScore = recipe.StarsCount * 2;
            int newScore = Mathf.Clamp(stars + result, 0, maxScore);
            _cookbook.SetRecipeStars(recipe, newScore);

            if (newScore <= 0)
            {
                _dishCrafter.MakeSlops(_activeDishRecipe);
                return;
            }
            
            if (newScore >= maxScore && !_autoCookbook.HasRecipe(recipe))
            {
                _autoCookbook.AddRecipe(recipe);
            }
            
            bool isExtra = stars == maxScore && newScore == maxScore;
            _dishCrafter.CraftDish(_activeDishRecipe, isExtra);
        }

        private void ProcessNewRecipe(int result)
        {
            if (!_matcher.MatchRecipe(_activeDishRecipe, out DishRecipe newRecipe) 
                || result != MiniGame.Results[Result.Green])
            {
                _dishCrafter.MakeSlops(_activeDishRecipe);
                return;
            }

            _cookbook.AddRecipe(newRecipe);
            _activeDishRecipe.Setup(newRecipe);
            _dishCrafter.CraftDish(_activeDishRecipe, isExtra: false);
        }
        

        private void OnValueChanged(float value) => OnGameValueChanged?.Invoke(value);

        private void Unsubscribe()
        {
            _game.OnValueChanged -= OnValueChanged;
            _game.OnTimeChanged -= OnGameTimeChanged;
            _game.OnTimeUp -= OnTimeUp;
        }

        private void OnGameTimeChanged(float time) => OnTimeChanged?.Invoke(time);

        private void OnTimeUp()
        {
            StopGame();
            if (_activeDishRecipe.IsEmpty)
            {
                _dishCrafter.MakeSlops(_activeDishRecipe);
                return;
            }
            
            DishRecipe recipe = _activeDishRecipe.Recipe;
            int stars = _cookbook.GetRecipeStars(recipe);
            int newScore = Mathf.Max(stars + MiniGame.Results[Result.Red], 0);
            _cookbook.SetRecipeStars(recipe, newScore);
            _dishCrafter.MakeSlops(_activeDishRecipe);
        }
    }
}