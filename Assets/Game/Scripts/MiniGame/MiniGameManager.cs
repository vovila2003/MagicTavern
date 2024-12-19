using System.Collections.Generic;
using Modules.GameCycle;
using Sirenix.OdinInspector;
using Tavern.Cooking;
using Tavern.InputServices.Interfaces;
using Tavern.MiniGame.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.MiniGame
{
    public class MiniGameManager : MonoBehaviour, ITickable
    {
        [SerializeField] 
        private MiniGameConfig Config;
        
        private readonly Dictionary<DishRecipe, int> _recipes = new();
        
        private MiniGame _game;
        private MiniGamePresenter _presenter;
        private GameCycle _gameCycle;
        private ISpaceInput _input;
        private IMiniGameView _view;
        private DishRecipe _currentRecipe;
        private DishCookbookContext _cookbook;

        [Inject]
        public void Construct(GameCycle gameCycle, ISpaceInput input, IMiniGameView view, DishCookbookContext cookbook)
        {
            _gameCycle = gameCycle;
            _input = input;
            _view = view;
            _cookbook = cookbook;
        }

        private void Start()
        {
            _game = new MiniGame(_input, Config);
            _gameCycle.AddListener(_game);
            _presenter = new MiniGamePresenter(_view, _game);
            _presenter.OnGameOver += OnGameOver;
            _presenter.OnGameRestart += OnRestartGame;
        }

        private void OnDisable()
        {
            _presenter.Dispose();
            _gameCycle.RemoveListener(_game);
            _presenter.OnGameOver -= OnGameOver;
            _presenter.OnGameRestart -= OnRestartGame;
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
            
            _presenter.Show(currentScore, Config.Match);
            _currentRecipe = recipe;
        }

        public void Tick() => _game?.Tick(Time.deltaTime);

        private void OnGameOver(bool win)
        {
            if (_currentRecipe is null) return;

            if (!win) return;
            
            _recipes.TryAdd(_currentRecipe, 0);
            _recipes[_currentRecipe]++;

            if (_recipes[_currentRecipe] < Config.Match) return;
            
            _cookbook.AddRecipe(_currentRecipe);
            _recipes.Remove(_currentRecipe);
        }

        private void OnRestartGame() => CreateGame(_currentRecipe);
    }
}