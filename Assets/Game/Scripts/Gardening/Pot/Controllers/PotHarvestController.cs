using Modules.Gardening;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.Gardening
{
    public sealed class PotHarvestController
    {
        private readonly Pot _pot;
        private readonly ProductInventoryContext _productsStorage;
        private readonly ISlopsStorage _slopeStorage;
        private readonly ISeedsStorage _seedsStorage;

       public PotHarvestController(
            Pot pot, 
            ProductInventoryContext productsStorage, 
            ISlopsStorage slopeStorage, 
            ISeedsStorage seedsStorage)
        {
            _pot = pot;
            _productsStorage = productsStorage;
            _slopeStorage = slopeStorage;
            _seedsStorage = seedsStorage;
            _pot.OnHarvestReceived += OnHarvestReceived;
            _pot.OnSlopsReceived += OnSlopsReceived;
        }

        public void Dispose()
        {
            _pot.OnHarvestReceived -= OnHarvestReceived;
            _pot.OnSlopsReceived -= OnSlopsReceived;
        }

        private void OnHarvestReceived(PlantConfig config, int value, bool hasSeed)
        {
            AddHarvestToProductStorage(config, value);

            if (hasSeed)
            {
                AddSeedToStorage(config.Plant, 1); //one seed in harvest
            }
        }

        private void AddHarvestToProductStorage(PlantConfig config, int value)
        {
            for (var i = 0; i < value; i++)
            {
                _productsStorage.AddItemByName(config.Name);
            }
        }

        private void AddSeedToStorage(Plant type, int count)
        {
            if (!_seedsStorage.TryGetStorage(type, out PlantStorage storage))
            {
                Debug.LogWarning($"Unknown seed storage of type {type.PlantName}");
                return;
            }
            
            storage.Add(count);
        }

        private void OnSlopsReceived(int value)
        {
            _slopeStorage.AddSlops(value);
        }
    }
}