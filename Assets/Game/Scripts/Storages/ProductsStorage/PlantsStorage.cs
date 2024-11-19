using System;
using System.Collections.Generic;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Storages
{
    public class PlantsStorage : MonoBehaviour
    {
        public event Action<PlantType, int> OnStorageValueAdded;
        public event Action<PlantType, int> OnStorageValueChanged;
        public event Action<PlantType, int> OnStorageValueSpent;
        public event Action<PlantType> OnStorageIsFull;
        public event Action<PlantType> OnStorageIsEmpty;

        [SerializeField] 
        private PlantStorage[] Storages;
        
        private readonly Dictionary<PlantType, PlantStorage> _storagesDictionary = new();
        
        public IReadOnlyList<PlantStorage> PlantStorages => Storages;
        
        private void OnValidate()
        {
            var collection = new Dictionary<PlantType, bool>();
            foreach (PlantStorage storage in Storages)
            {
                if (collection.TryAdd(storage.PlantType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate plant storage of type {storage.PlantType}");
            }            
        }

        private void OnEnable()
        {
            foreach (PlantStorage storage in Storages)
            {
                storage.Init();
                PlantType type = storage.PlantType;
                _storagesDictionary.Add(type, storage);
                Subscribe(storage);
            }
        }

        private void OnDisable()
        {
            foreach (PlantStorage storage in Storages)
            {
                Unsubscribe(storage);
                storage.Dispose();
            }
        }

        public bool TryGetStorage(PlantType type, out PlantStorage storage)
        {
            return _storagesDictionary.TryGetValue(type, out storage);
        }

        private void Subscribe(PlantStorage storage)
        {
            storage.OnPlantStorageAdded += OnValueAdded;
            storage.OnPlantStorageChanged += OnValueChanged;
            storage.OnPlantStorageValueSpent += OnValueSpent;
            storage.OnPlantStorageEmpty += OnEmpty;
            storage.OnPlantStorageFull += OnFull;
        }

        private void Unsubscribe(PlantStorage storage)
        {
            storage.OnPlantStorageAdded -= OnValueAdded;
            storage.OnPlantStorageChanged -= OnValueChanged;
            storage.OnPlantStorageValueSpent -= OnValueSpent;
            storage.OnPlantStorageEmpty -= OnEmpty;
            storage.OnPlantStorageFull -= OnFull;
        }

        private void OnValueAdded(PlantType type, int value) => OnStorageValueAdded?.Invoke(type, value);

        private void OnValueChanged(PlantType type, int value) => OnStorageValueChanged?.Invoke(type, value);

        private void OnValueSpent(PlantType type, int value) => OnStorageValueSpent?.Invoke(type, value);

        private void OnEmpty(PlantType type) => OnStorageIsEmpty?.Invoke(type);

        private void OnFull(PlantType type) => OnStorageIsFull?.Invoke(type);
    }
}