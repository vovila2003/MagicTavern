using Modules.GameCycle;
using Sirenix.OdinInspector;
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
        
        private MiniGame _game;
        private MiniGamePresenter _presenter;
        private GameCycle _gameCycle;
        private IObjectResolver _container;

        [Inject]
        public void Construct(GameCycle gameCycle, IObjectResolver container)
        {
            _gameCycle = gameCycle;
            _container = container;
        }

        private void Start()
        {
            var input = _container.Resolve<ISpaceInput>(); 
            var view = _container.Resolve<IMiniGameView>();    
                
            _game = new MiniGame(input, Config);
            _gameCycle.AddListener(_game);
            _presenter = new MiniGamePresenter(view, _game);
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

        public void Tick()
        {
            _game?.Tick(Time.deltaTime);
        }
    }
}