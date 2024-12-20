using Modules.Gardening;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.Gardening
{
    public sealed class PotHarvestController
    {
        private readonly Pot _pot;
        private readonly IProductsStorage _productsStorage;
        private readonly ISlopsStorage _slopeStorage;
        private readonly ISeedsStorage _seedsStorage;

       public PotHarvestController(
            Pot pot, 
            IProductsStorage productsStorage, 
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

        private void OnHarvestReceived(Plant type, int value, bool hasSeed)
        {
            AddHarvestToProductStorage(type, value);

            if (hasSeed)
            {
                AddSeedToStorage(type, 1); //one seed in harvest
            }
        }

        private void AddHarvestToProductStorage(Plant type, int value)
        {
            if (!_productsStorage.TryGetStorage(type, out PlantStorage storage))
            {
                Debug.LogWarning($"Unknown storage of type {type.PlantName}");
                return;
            }

            storage.Add(value);
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