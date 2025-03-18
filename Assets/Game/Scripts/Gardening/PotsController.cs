using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Gardening
{
    [UsedImplicitly]
    public class PotsController :
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        ITickable
    {
        private readonly PotFactory _factory;
        private bool _isEnable;

        public PotsController(PotFactory factory)
        {
            _factory = factory;
        }

        void ITickable.Tick()
        {
            if (!_isEnable) return;

            foreach (Pot pot in _factory.Pots)
            {
                pot.Tick(Time.deltaTime);
            }
        }
        
        void IStartGameListener.OnStart()
        {
            _isEnable = true;
            foreach (Pot pot in _factory.Pots)
            {
                pot.OnStart();
            }
        }
        
        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
            foreach (Pot pot in _factory.Pots)
            {
                pot.OnPause();
            }
        }
        
        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
            foreach (Pot pot in _factory.Pots)
            {
                pot.OnResume();
            }
        }
        
        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            foreach (Pot pot in _factory.Pots)
            {
                pot.OnFinish();
            }
        }
    }
}