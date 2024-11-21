using System;
using Modules.Gardening;
using Tavern.Storages;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Tavern.Gardening
{
    public sealed class SeedbedHarvestController : MonoBehaviour
    {
        [SerializeField]
        private Seedbed Seedbed;
        
        private IProductsStorage _productsStorage;
        private ISlopsStorage _slopeStorage;

        [Inject]
        public void Construct(IProductsStorage productsStorage, ISlopsStorage slopeStorage)
        {
            _productsStorage = productsStorage;
            _slopeStorage = slopeStorage;
        }

        private void OnEnable()
        {
            Seedbed.OnHarvestReceived += OnHarvestReceived;
            Seedbed.OnSlopsReceived += OnSlopsReceived;
        }

        private void OnDisable()
        {
            Seedbed.OnHarvestReceived -= OnHarvestReceived;
                        
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
            _slopeStorage.Add(value);
        }
    }
}