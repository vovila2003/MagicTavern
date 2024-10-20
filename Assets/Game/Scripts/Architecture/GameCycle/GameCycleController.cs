using System.Collections.Generic;
using Tavern.Architecture.GameManager.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Architecture
{
    public class GameCycleController:
        IInitializable, 
        ITickable, 
        IFixedTickable,
        ILateTickable
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
            var updateListeners = _container.Resolve<IEnumerable<IUpdateListener>>();
            var fixedUpdateListeners = _container.Resolve<IEnumerable<IFixedUpdateListener>>();
            var lateUpdateListeners = _container.Resolve<IEnumerable<ILateUpdateListener>>();
            
            _gameCycle.Initialize(listeners, updateListeners, fixedUpdateListeners, lateUpdateListeners);
        }

        void ITickable.Tick()
        {
            float time = Time.deltaTime;
            _gameCycle.Tick(time);
        }

        void IFixedTickable.FixedTick()
        {
            float time = Time.fixedDeltaTime;
            _gameCycle.FixedTick(time);
        }

        void ILateTickable.LateTick()
        {
            float time = Time.deltaTime;
            _gameCycle.LateTick(time);
        }
    }
}