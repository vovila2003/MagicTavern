using Modules.Gardening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class ResourcesStorageContext : MonoBehaviour
    {
        [SerializeField]
        private ResourcesStorage Storages;

        private void OnEnable()
        {
            Storages.OnStorageValueAdded += ValueAdded;
            Storages.OnStorageValueChanged += ValueChanged;
            Storages.OnStorageIsEmpty += OnEmpty;
            Storages.OnStorageIsFull += OnFull;
            Storages.OnStorageValueSpent += OnSpent;
        }

        private void OnDisable()
        {
            Storages.OnStorageValueAdded -= ValueAdded;
            Storages.OnStorageValueChanged -= ValueChanged;
            Storages.OnStorageIsEmpty -= OnEmpty;
            Storages.OnStorageIsFull -= OnFull;
            Storages.OnStorageValueSpent -= OnSpent;
        }

        private void ValueAdded(Caring type, float value)
        {
            Debug.Log($"Storage {type.CaringName} added by {value}");
        }

        private void ValueChanged(Caring type, float value)
        {
            Debug.Log($"Storage {type.CaringName} value changed to {value}");
        }

        private void OnSpent(Caring type, float value)
        {
            Debug.Log($"Storage {type.CaringName} spent by {value}");
        }

        private void OnFull(Caring type)
        {
            Debug.Log($"Storage {type.CaringName} is full");
        }

        private void OnEmpty(Caring type)
        {
            Debug.Log($"Storage {type.CaringName} is empty");
        }

        [Button]
        public void Add(Caring type, float value)
        {
            bool result = false;
            if (Storages.TryGetStorage(type, out ResourceStorage storage))
            {
                result = storage.Add(value);
            }
            
            Debug.Log($"Add to {type.CaringName} storage value {value}: result - {result}");
        }

        [Button]
        public void Spend(Caring type, float value)
        {
            bool result = false;
            if (Storages.TryGetStorage(type, out ResourceStorage storage))
            {
                result = storage.Spend(value);
            }
            
            Debug.Log($"Spend from {type.CaringName} storage value {value}: result - {result}");
        }

        [Button]
        public void ResetStorage(Caring type)
        {
            bool getStorage = Storages.TryGetStorage(type, out ResourceStorage storage);
            if (getStorage)
            {
                storage.Reset();
            }
            
            Debug.Log($"Reset {type.CaringName} storage : result - {getStorage}");
        }
    }
}