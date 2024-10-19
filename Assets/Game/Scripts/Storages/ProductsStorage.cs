using System;
using System.Collections.Generic;
using Modules.Products.Plants;
using UnityEngine;

namespace Tavern.Storages
{
    public class ProductsStorage : MonoBehaviour
    {
        public event Action<PlantType, int> OnStorageAdded;
        public event Action<PlantType, int> OnStorageChanged;
        public event Action<PlantType, int> OnStorageValueSpent;
        public event Action<PlantType> OnStorageIsFull;
        public event Action<PlantType> OnStorageIsEmpty;

        [SerializeField] 
        private PlantStorage[] Storages;
        
        private readonly Dictionary<PlantType, PlantStorage> _storagesDictionary = new();
        
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
            storage.OnPlantStorageAdded += OnStorageAdded;
            storage.OnPlantStorageChanged += OnStorageChanged;
            storage.OnPlantStorageEmpty += OnStorageIsEmpty;
            storage.OnPlantStorageFull += OnStorageIsFull;
            storage.OnPlantStorageValueSpent += OnStorageValueSpent;
        }

        private void Unsubscribe(PlantStorage storage)
        {
            storage.OnPlantStorageAdded -= OnStorageAdded;
            storage.OnPlantStorageChanged -= OnStorageChanged;
            storage.OnPlantStorageEmpty -= OnStorageIsEmpty;
            storage.OnPlantStorageFull -= OnStorageIsFull;
            storage.OnPlantStorageValueSpent -= OnStorageValueSpent;
        }
    }
}