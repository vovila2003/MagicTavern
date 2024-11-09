using System.Collections.Generic;
using Modules.GameCycle;
using Tavern.Settings;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.Gardening
{
    public class SeedbedFactory
    {
        private readonly IProductsStorage _productsStorage;
        private readonly GameCycle _gameCycle;
        private readonly GameObject _seedbedPrefab;
        private readonly Transform _parent;
        private readonly Dictionary<Seedbed, SeedbedHarvestController> _controllers = new();

        public SeedbedFactory(SeedbedSettings settings, IProductsStorage productsStorage, GameCycle gameCycle)
        {
            _seedbedPrefab = settings.Seedbed;
            _parent = settings.Parent;
            _productsStorage = productsStorage;
            _gameCycle = gameCycle;
        }

        public Seedbed CreateSeedbed(Vector3 position, Quaternion rotation)
        {
            GameObject instance = Object.Instantiate(_seedbedPrefab, position, rotation, _parent);
            var seedbed = instance.GetComponent<Seedbed>();
            _gameCycle.AddListener(seedbed);
            
            var controller = new SeedbedHarvestController(seedbed, _productsStorage);
            
            _controllers.Add(seedbed, controller);
            seedbed.OnDestroyed += OnSeedbedDestroyed;
            
            return seedbed;
        }

        private void OnSeedbedDestroyed(Seedbed seedbed)
        {
            _gameCycle.RemoveListener(seedbed);
            if (!_controllers.TryGetValue(seedbed, out SeedbedHarvestController controller))
            {
                return;
            }
            
            controller.Dispose();
            _controllers.Remove(seedbed);
        }
    }
}