using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
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
        private readonly IObjectResolver _resolver;
        private Pot _prefab;
        private PlantProductInventoryContext _plantProductsStorage;
        private ISlopsStorage _slopsStorage;
        private SeedInventoryContext _seedsStorage;
        private SceneSettings _sceneSettings;
        private GameCycle _gameCycle;
        private IUiManager _uiManager;

        private readonly Dictionary<Pot, PotHarvestController> _pots = new();
        private readonly Dictionary<Pot, PotListener> _listeners = new();

        public Dictionary<Pot, PotHarvestController>.KeyCollection Pots => _pots.Keys;

        public PotFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Create(Vector3 position)
        {
            Pot pot = _resolver.Instantiate(_prefab, position, Quaternion.identity, _sceneSettings.PotsParent);
            pot.Setup();
            var controller = new PotHarvestController(pot, _plantProductsStorage, _slopsStorage, _seedsStorage);
            _pots.Add(pot, controller);
            _listeners.Add(pot, new PotListener(pot, _gameCycle, _uiManager));
        }

        public void Destroy(Pot pot)
        {
            DisposeListener(pot);
            DisposeController(pot);
        }

        void IInitGameListener.OnInit()
        {
            InitFields();
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

        private void InitFields()
        {
            _plantProductsStorage ??= _resolver.Resolve<PlantProductInventoryContext>();
            _slopsStorage ??= _resolver.Resolve<ISlopsStorage>();
            _seedsStorage = _resolver.Resolve<SeedInventoryContext>();
            _sceneSettings = _resolver.Resolve<SceneSettings>();
            _gameCycle = _resolver.Resolve<GameCycle>();
            _uiManager = _resolver.Resolve<IUiManager>();
            _prefab = _resolver.Resolve<GameSettings>().GardeningSettings.Pot;
        }
    }
}