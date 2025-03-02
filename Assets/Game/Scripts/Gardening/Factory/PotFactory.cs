using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Infrastructure;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using Tavern.Storages;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Gardening
{
    [UsedImplicitly]
    public class PotFactory :
        IInitGameListener,
        IExitGameListener
    {
        private readonly Pot _prefab;
        private readonly IObjectResolver _resolver;
        private readonly PlantProductInventoryContext _plantProductsStorage;
        private readonly ISlopsStorage _slopsStorage;
        private readonly SeedInventoryContext _seedsStorage;
        private readonly SceneSettings _sceneSettings;
        private readonly GameCycleController _gameCycleController;
        private readonly IUiManager _uiManager;

        private readonly Dictionary<Pot, PotHarvestController> _pots = new();
        private readonly Dictionary<Pot, PotListener> _listeners = new();

        public Dictionary<Pot, PotHarvestController>.KeyCollection Pots => _pots.Keys;

        public PotFactory(
            PlantProductInventoryContext plantProductsStorage, 
            ISlopsStorage slopsStorage,
            SeedInventoryContext seedsStorage,
            GameSettings settings, 
            SceneSettings sceneSettings,
            GameCycleController gameCycleController,
            IUiManager uiManager,
            IObjectResolver resolver)
        {
            _plantProductsStorage = plantProductsStorage;
            _slopsStorage = slopsStorage;
            _seedsStorage = seedsStorage;
            _sceneSettings = sceneSettings;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;
            _prefab = settings.GardeningSettings.Pot;
            _resolver = resolver;
        }

        public void Create(Vector3 position)
        {
            Pot pot = _resolver.Instantiate(_prefab, position, Quaternion.identity, _sceneSettings.PotsParent);
            pot.Setup();
            var controller = new PotHarvestController(pot, _plantProductsStorage, _slopsStorage, _seedsStorage);
            _pots.Add(pot, controller);
            _listeners.Add(pot, new PotListener(pot, _gameCycleController, _uiManager));
        }

        public void Destroy(Pot pot)
        {
            DisposeListener(pot);
            DisposeController(pot);
        }

        private void DisposeController(Pot pot)
        {
            if (!_pots.Remove(pot, out PotHarvestController controller)) return;
            
            controller.Dispose();
            Object.Destroy(pot.gameObject);
        }

        private void DisposeListener(Pot pot)
        {
            if (!_listeners.Remove(pot, out PotListener listener)) return;

            listener.Dispose();
        }

        void IInitGameListener.OnInit()
        {
            foreach (PotPoint point in _sceneSettings.PotPoints)
            {
                Create(point.transform.position);
            }
            
            foreach (PotPoint point in _sceneSettings.PotPoints)
            {
                Object.Destroy(point.gameObject);
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (PotHarvestController controller in _pots.Values)
            {
                controller.Dispose();
            }
            
            _pots.Clear();
            
            foreach (PotListener listener in _listeners.Values)
            {
                listener.Dispose();
            }
            
            _listeners.Clear();
        }
    }
}