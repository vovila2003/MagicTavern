using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.ProductsAndIngredients;
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
        private readonly PlantProductInventoryContext _plantProductsStorage;
        private readonly ISlopsStorage _slopsStorage;
        private readonly SeedInventoryContext _seedsStorage;

        private readonly Dictionary<Pot, PotHarvestController> _pots = new();
        private bool _isEnable;

        public PotsController(
            PlantProductInventoryContext plantProductsStorage, 
            ISlopsStorage slopsStorage,
            SeedInventoryContext seedsStorage,
            Pot prefab, 
            Transform parent)
        {
            _plantProductsStorage = plantProductsStorage;
            _slopsStorage = slopsStorage;
            _seedsStorage = seedsStorage;
            _prefab = prefab;
            _parent = parent;
        }

        public void Create(Vector3 position)
        {
            Pot pot = Object.Instantiate(_prefab, position, Quaternion.identity, _parent);
            var controller = new PotHarvestController(pot, _plantProductsStorage, _slopsStorage, _seedsStorage);
            _pots.Add(pot, controller);
        }

        public void Destroy(Pot pot)
        {
            if (!_pots.Remove(pot, out PotHarvestController controller)) return;
            
            controller.Dispose();
            Object.Destroy(pot.gameObject);
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