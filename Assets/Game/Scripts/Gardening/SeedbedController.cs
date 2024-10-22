using Modules.Gardening;
using Tavern.Architecture.GameManager.Interfaces;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.Gardening
{
    public sealed class SeedbedController :
        IStartGameListener,
        IFinishGameListener
    {
        private readonly SeedbedTest _seedbed;
        private readonly IProductsStorage _productsStorage;

        public SeedbedController(SeedbedTest seedbed, IProductsStorage productsStorage)
        {
            _seedbed = seedbed;
            _productsStorage = productsStorage;
        }

        public void OnStart()
        {
            _seedbed.OnHarvestReceived += OnHarvestReceived;
        }

        public void OnFinish()
        {
            _seedbed.OnHarvestReceived -= OnHarvestReceived;
        }

        private void OnHarvestReceived(PlantType type, int value)
        {
            if (!_productsStorage.TryGetStorage(type, out PlantStorage storage))
            {
                Debug.LogWarning($"Unknown storage of type {type}");
                return;
            }

            storage.Add(value);
        }
    }
}