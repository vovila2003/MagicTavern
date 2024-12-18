using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using Tavern.Settings;
using Tavern.Storages;
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
        private readonly Pot _prefab;
        private readonly Transform _parent;
        private readonly IProductsStorage _productsStorage;
        private readonly ISlopsStorage _slopsStorage;
        private readonly ISeedsStorage _seedsStorage;

        private readonly Dictionary<Pot, PotHarvestController> _pots = new();
        private bool _isEnable;

        public PotsController(
            IProductsStorage productsStorage, 
            ISlopsStorage slopsStorage,
            ISeedsStorage seedsStorage,
            PotSettings settings, 
            Transform parent, 
            GameCycle cycle)
        {
            _productsStorage = productsStorage;
            _slopsStorage = slopsStorage;
            _seedsStorage = seedsStorage;
            _prefab = settings.Pot;
            _parent = parent;
        }

        public void Create(Vector3 position)
        {
            Pot pot = Object.Instantiate(_prefab, position, Quaternion.identity, _parent);
            var controller = new PotHarvestController(pot, _productsStorage, _slopsStorage, _seedsStorage);
            _pots.Add(pot, controller);
        }

        public void Destroy(Pot pot)
        {
            if (!_pots.Remove(pot, out PotHarvestController controller)) return;
            
            controller.Dispose();
            Object.Destroy(pot.gameObject); // через пул
        }

        void ITickable.Tick()
        {
            if (!_isEnable) return;

            foreach (Pot pot in _pots.Keys)
            {
                pot.Tick(Time.deltaTime);
            }
        }
        
        void IStartGameListener.OnStart()
        {
            _isEnable = true;
            foreach (Pot pot in _pots.Keys)
            {
                pot.OnStart();
            }
        }
        
        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
            foreach (Pot pot in _pots.Keys)
            {
                pot.OnPause();
            }
        }
        
        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
            foreach (Pot pot in _pots.Keys)
            {
                pot.OnResume();
            }
        }
        
        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            foreach (Pot pot in _pots.Keys)
            {
                pot.OnFinish();
            }
        }
    }
}