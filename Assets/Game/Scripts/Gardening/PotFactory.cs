using System.Collections.Generic;
using JetBrains.Annotations;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using Tavern.Storages;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Gardening
{
    [UsedImplicitly]
    public class PotFactory 
    {
        private readonly Pot _prefab;
        private readonly Transform _parent;
        private readonly IObjectResolver _resolver;
        private readonly PlantProductInventoryContext _plantProductsStorage;
        private readonly ISlopsStorage _slopsStorage;
        private readonly SeedInventoryContext _seedsStorage;

        private readonly Dictionary<Pot, PotHarvestController> _pots = new();

        public Dictionary<Pot, PotHarvestController>.KeyCollection Pots => _pots.Keys;

        public PotFactory(
            PlantProductInventoryContext plantProductsStorage, 
            ISlopsStorage slopsStorage,
            SeedInventoryContext seedsStorage,
            GameSettings settings, 
            SceneSettings sceneSettings,
            IObjectResolver resolver)
        {
            _plantProductsStorage = plantProductsStorage;
            _slopsStorage = slopsStorage;
            _seedsStorage = seedsStorage;
            _prefab = settings.GardeningSettings.Pot;
            _parent = sceneSettings.PotsParent;
            _resolver = resolver;
        }

        public void Create(Vector3 position)
        {
            Pot pot = _resolver.Instantiate(_prefab, position, Quaternion.identity, _parent);
            var controller = new PotHarvestController(pot, _plantProductsStorage, _slopsStorage, _seedsStorage);
            _pots.Add(pot, controller);
        }

        public void Destroy(Pot pot)
        {
            if (!_pots.Remove(pot, out PotHarvestController controller)) return;
            
            controller.Dispose();
            Object.Destroy(pot.gameObject);
        }
    }
}