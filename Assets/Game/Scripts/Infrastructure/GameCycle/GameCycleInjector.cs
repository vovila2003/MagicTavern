using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class GameCycleInjector: IInitializable 
    {
        private readonly IObjectResolver _container;
        private readonly GameCycle _gameCycle;
        private readonly TimeGameCycle _timeGameCycle;

        public GameCycleInjector(
            IObjectResolver container, 
            GameCycle gameCycle, 
            TimeGameCycle timeGameCycle)
        {
            _container = container;
            _gameCycle = gameCycle;
            _timeGameCycle = timeGameCycle;
        }

        void IInitializable.Initialize()
        {
            var listeners = _container.Resolve<IEnumerable<IGameListener>>();
            _gameCycle.Initialize(listeners);
            
            var timeListeners = _container.Resolve<IEnumerable<ITimeListener>>();
            _timeGameCycle.Initialize(timeListeners);
        }
    }
}