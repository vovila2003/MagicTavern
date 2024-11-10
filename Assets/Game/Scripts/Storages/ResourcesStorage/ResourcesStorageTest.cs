using Modules.Gardening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class ResourcesStorageTest : MonoBehaviour
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

        private void ValueAdded(CaringType type, float value)
        {
            Debug.Log($"Storage {type} added by {value}");
        }

        private void ValueChanged(CaringType type, float value)
        {
            Debug.Log($"Storage {type} value changed to {value}");
        }

        private void OnSpent(CaringType type, float value)
        {
            Debug.Log($"Storage {type} spent by {value}");
        }

        private void OnFull(CaringType type)
        {
            Debug.Log($"Storage {type} is full");
        }

        private void OnEmpty(CaringType type)
        {
            Debug.Log($"Storage {type} is empty");
        }

        [Button]
        public void Add(CaringType type, float value)
        {
            bool result = false;
            if (Storages.TryGetStorage(type, out ResourceStorage storage))
            {
                result = storage.Add(value);
            }
            
            Debug.Log($"Add to {type} storage value {value}: result - {result}");
        }

        [Button]
        public void Spend(CaringType type, float value)
        {
            bool result = false;
            if (Storages.TryGetStorage(type, out ResourceStorage storage))
            {
                result = storage.Spend(value);
            }
            
            Debug.Log($"Spend from {type} storage value {value}: result - {result}");
        }

        [Button]
        public void ResetStorage(CaringType type)
        {
            bool getStorage = Storages.TryGetStorage(type, out ResourceStorage storage);
            if (getStorage)
            {
                storage.Reset();
            }
            
            Debug.Log($"Reset {type} storage : result - {getStorage}");
        }
    }
}