using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Gardening;
using Modules.Storages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class PlantsStorage : MonoBehaviour
    {
        public event Action<Plant, int> OnStorageValueAdded;
        public event Action<Plant, int> OnStorageValueChanged;
        public event Action<Plant, int> OnStorageValueSpent;
        public event Action<Plant> OnStorageIsFull;
        public event Action<Plant> OnStorageIsEmpty;

        [SerializeField] 
        private PlantStorage[] Storages;
        
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<Plant, PlantStorage> _storagesDictionary = new();
        
        public IReadOnlyList<PlantStorage> PlantStorages => _storagesDictionary.Values.ToList();
        
        private void OnValidate()
        {
            var collection = new Dictionary<Plant, bool>();
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
                Plant type = storage.PlantType;
                _storagesDictionary.Add(type, storage);
                Subscribe(storage);
            }
        }

        private void OnDisable()
        {
            foreach (PlantStorage storage in PlantStorages)
            {
                Unsubscribe(storage);
                storage.Dispose();
            }
        }

        public bool TryGetStorage(Plant type, out PlantStorage storage)
        {
            return _storagesDictionary.TryGetValue(type, out storage);
        }

        public PlantStorage CreateStorage(
            PlantConfig config, 
            LimitType limitType = LimitType.Unlimited, 
            int maxValue = 0)
        {
            var storage = new PlantStorage(config, limitType, maxValue);
            storage.Init();
            _storagesDictionary.Add(config.Plant, storage);
            Subscribe(storage);
            return storage;
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

        private void OnValueAdded(Plant type, int value) => OnStorageValueAdded?.Invoke(type, value);

        private void OnValueChanged(Plant type, int value) => OnStorageValueChanged?.Invoke(type, value);

        private void OnValueSpent(Plant type, int value) => OnStorageValueSpent?.Invoke(type, value);

        private void OnEmpty(Plant type) => OnStorageIsEmpty?.Invoke(type);

        private void OnFull(Plant type) => OnStorageIsFull?.Invoke(type);
    }
}