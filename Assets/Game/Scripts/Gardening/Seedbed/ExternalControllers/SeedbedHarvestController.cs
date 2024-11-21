using Modules.Gardening;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.Gardening
{
    public sealed class SeedbedHarvestController
    {
        private readonly Seedbed _seedbed;
        private readonly IProductsStorage _productsStorage;

        public SeedbedHarvestController(Seedbed seedbed, IProductsStorage productsStorage)
        {
            _seedbed = seedbed;
            _productsStorage = productsStorage;
            _seedbed.OnHarvestReceived += OnHarvestReceived;
            _seedbed.OnSlopsReceived += OnSlopsReceived;
        }

        public void Dispose()
        {
            _seedbed.OnHarvestReceived -= OnHarvestReceived;
        }

        private void OnHarvestReceived(Plant type, int value)
        {
            if (!_productsStorage.TryGetStorage(type, out PlantStorage storage))
            {
                Debug.LogWarning($"Unknown storage of type {type.PlantName}");
                return;
            }

            storage.Add(value);
        }

        private void OnSlopsReceived(int value)
        {
            //TODO
            Debug.Log($"Received {value} slops");
        }
    }
}