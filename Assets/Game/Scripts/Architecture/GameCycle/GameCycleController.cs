using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Tavern.Architecture
{
    [UsedImplicitly]
    public class GameCycleController: IInitializable 
        
    {
        private readonly IObjectResolver _container;
        private readonly Modules.GameCycle.GameCycle _gameCycle;

        public GameCycleController(IObjectResolver container, Modules.GameCycle.GameCycle gameCycle)
        {
            _container = container;
            _gameCycle = gameCycle;
        }

        public void PrepareGame() => _gameCycle.PrepareGame();

        public void StartGame() => _gameCycle.StartGame();

        public void FinishGame() => _gameCycle.FinishGame();

        public void PauseGame() => _gameCycle.PauseGame();

        public void ResumeGame() => _gameCycle.ResumeGame();

        public void ExitGame() => _gameCycle.ExitGame();

        void IInitializable.Initialize()
        {
            var listeners = _container.Resolve<IEnumerable<IGameListener>>();
            
            _gameCycle.Initialize(listeners);
        }
    }
}