using System;
using System.Collections.Generic;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Storages
{
    public class ResourcesStorage : MonoBehaviour, IResourcesStorage
    {
        public event Action<Caring, float> OnStorageValueAdded;
        public event Action<Caring, float> OnStorageValueChanged;
        public event Action<Caring, float> OnStorageValueSpent;
        public event Action<Caring> OnStorageIsFull;
        public event Action<Caring> OnStorageIsEmpty;

        [SerializeField] 
        private ResourceStorage[] Storages;
        
        private readonly Dictionary<Caring, ResourceStorage> _storagesDictionary = new();
        
        private void OnValidate()
        {
            var collection = new Dictionary<Caring, bool>();
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
                Caring type = storage.ResourceType;
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

        public bool TryGetStorage(Caring type, out ResourceStorage storage)
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

        private void OnValueAdded(Caring type, float value) => OnStorageValueAdded?.Invoke(type, value);

        private void OnValueChanged(Caring type, float value) => OnStorageValueChanged?.Invoke(type, value);

        private void OnValueSpent(Caring type, float value) => OnStorageValueSpent?.Invoke(type, value);

        private void OnEmpty(Caring type) => OnStorageIsEmpty?.Invoke(type);

        private void OnFull(Caring type) => OnStorageIsFull?.Invoke(type);
    }
}