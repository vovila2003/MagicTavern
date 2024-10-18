using System;
using System.Collections.Generic;
using Modules.Products.Plants;
using Modules.Storages;
using UnityEngine;

namespace Tavern.Storages
{
    public class ProductStorage : MonoBehaviour
    {
        public event Action<PlantType, int> OnStorageAdded;
        public event Action<PlantType, int> OnStorageChanged;
        public event Action<PlantType, int> OnStorageValueSpent;
        public event Action<PlantType> OnStorageIsFull;
        public event Action<PlantType> OnStorageIsEmpty;
        
        private readonly Dictionary<PlantType, StorageInt> _storages = new();
        
        private readonly StorageInt _wheatStorage = new();

        private void Start()
        {
            _wheatStorage.OnValueAdded += OnWheatStorageValueAdded;
            _wheatStorage.OnValueChange += OnWheatStorageValueChanged;
            _wheatStorage.OnValueSpent += OnWheatStorageValueSpent;
            _wheatStorage.OnStorageIsFull += OnWheatStorageIsFull;
            _wheatStorage.OnStorageIsEmpty += OnWheatStorageIsEmpty;
            _storages[PlantType.Wheat] = _wheatStorage;
            
        }

        private void OnDisable()
        {
            _wheatStorage.OnValueAdded -= OnWheatStorageValueAdded;
            _wheatStorage.OnValueChange -= OnWheatStorageValueChanged;
            _wheatStorage.OnValueSpent -= OnWheatStorageValueSpent;
            _wheatStorage.OnStorageIsFull -= OnWheatStorageIsFull;
            _wheatStorage.OnStorageIsEmpty -= OnWheatStorageIsEmpty;
        }

        private void OnWheatStorageValueAdded(int value) => OnStorageAdded?.Invoke(PlantType.Wheat, value);

        private void OnWheatStorageValueChanged(int value) => OnStorageChanged?.Invoke(PlantType.Wheat, value);

        private void OnWheatStorageValueSpent(int value) => OnStorageValueSpent?.Invoke(PlantType.Wheat, value);

        private void OnWheatStorageIsFull() => OnStorageIsFull?.Invoke(PlantType.Wheat);

        private void OnWheatStorageIsEmpty() => OnStorageIsEmpty?.Invoke(PlantType.Wheat);
    }
}