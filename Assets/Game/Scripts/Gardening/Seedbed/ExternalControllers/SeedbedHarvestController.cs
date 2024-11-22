using System;
using Modules.Gardening;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    [RequireComponent(typeof(Seedbed))]
    public sealed class SeedbedHarvestController : MonoBehaviour
    {
        private Seedbed _seedbed;
        private IProductsStorage _productsStorage;
        private ISlopsStorage _slopeStorage;

        [Inject]
        public void Construct(IProductsStorage productsStorage, ISlopsStorage slopeStorage)
        {
            _productsStorage = productsStorage;
            _slopeStorage = slopeStorage;
        }

        private void Awake()
        {
            _seedbed = GetComponent<Seedbed>();
        }

        private void OnEnable()
        {
            _seedbed.OnHarvestReceived += OnHarvestReceived;
            _seedbed.OnSlopsReceived += OnSlopsReceived;
        }

        private void OnDisable()
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
            _slopeStorage.Add(value);
        }
    }
}