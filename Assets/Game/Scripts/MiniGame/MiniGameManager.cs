using Modules.GameCycle;
using Sirenix.OdinInspector;
using Tavern.InputServices.Interfaces;
using Tavern.MiniGame.UI;
using UnityEngine;
using VContainer;

namespace Tavern.MiniGame
{
    public class MiniGameManager : MonoBehaviour
    {
        [SerializeField] 
        private MiniGameConfig Config;
        
        private IMiniGameView _view;
        private ISpaceInput _input;
        private MiniGame _game;
        private MiniGamePresenter _presenter;
        private GameCycle _gameCycle;

        [Inject]
        public void Construct(IMiniGameView view, ISpaceInput input, GameCycle gameCycle)
        {
            _view = view;
            _input = input;
            _gameCycle = gameCycle;
        }

        private void Start()
        {
            _game = new MiniGame(_input, Config);
            _gameCycle.AddListener(_game);
            _presenter = new MiniGamePresenter(_view, _game);
        }

        private void OnDisable()
        {
            _presenter.Dispose();
            _gameCycle.RemoveListener(_game);
        }

        [Button]
        public void CreateGame()
        {
            _presenter.Show();
        }
    }
}