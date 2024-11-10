using System;
using System.Collections.Generic;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Storages
{
    public class ResourcesStorage : MonoBehaviour, IResourcesStorage
    {
        public event Action<CaringType, float> OnStorageValueAdded;
        public event Action<CaringType, float> OnStorageValueChanged;
        public event Action<CaringType, float> OnStorageValueSpent;
        public event Action<CaringType> OnStorageIsFull;
        public event Action<CaringType> OnStorageIsEmpty;

        [SerializeField] 
        private ResourceStorage[] Storages;
        
        private readonly Dictionary<CaringType, ResourceStorage> _storagesDictionary = new();
        
        private void OnValidate()
        {
            var collection = new Dictionary<CaringType, bool>();
            foreach (ResourceStorage storage in Storages)
            {
                if (collection.TryAdd(storage.ResourceType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate plant storage of type {storage.ResourceType}");
            }            
        }

        private void OnEnable()
        {
            foreach (ResourceStorage storage in Storages)
            {
                storage.Init();
                CaringType type = storage.ResourceType;
                _storagesDictionary.Add(type, storage);
                Subscribe(storage);
            }
        }

        private void OnDisable()
        {
            foreach (ResourceStorage storage in Storages)
            {
                Unsubscribe(storage);
                storage.Dispose();
            }
        }

        public bool TryGetStorage(CaringType type, out ResourceStorage storage)
        {
            return _storagesDictionary.TryGetValue(type, out storage);
        }

        private void Subscribe(ResourceStorage storage)
        {
            storage.OnResourceStorageAdded += OnValueAdded;
            storage.OnResourceStorageChanged += OnValueChanged;
            storage.OnResourceStorageValueSpent += OnValueSpent;
            storage.OnResourceStorageEmpty += OnEmpty;
            storage.OnResourceStorageFull += OnFull;
        }

        private void Unsubscribe(ResourceStorage storage)
        {
            storage.OnResourceStorageAdded -= OnValueAdded;
            storage.OnResourceStorageChanged -= OnValueChanged;
            storage.OnResourceStorageValueSpent -= OnValueSpent;
            storage.OnResourceStorageEmpty -= OnEmpty;
            storage.OnResourceStorageFull -= OnFull;
        }

        private void OnValueAdded(CaringType type, float value) => OnStorageValueAdded?.Invoke(type, value);

        private void OnValueChanged(CaringType type, float value) => OnStorageValueChanged?.Invoke(type, value);

        private void OnValueSpent(CaringType type, float value) => OnStorageValueSpent?.Invoke(type, value);

        private void OnEmpty(CaringType type) => OnStorageIsEmpty?.Invoke(type);

        private void OnFull(CaringType type) => OnStorageIsFull?.Invoke(type);
    }
}