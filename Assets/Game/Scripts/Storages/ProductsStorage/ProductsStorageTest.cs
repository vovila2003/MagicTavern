using Modules.Gardening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class ProductsStorageTest : MonoBehaviour
    {
        [SerializeField]
        private ProductsStorage Storages;

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

        private void ValueAdded(PlantType type, int value)
        {
            Debug.Log($"Storage {type} added by {value}");
        }

        private void ValueChanged(PlantType type, int value)
        {
            Debug.Log($"Storage {type} value changed to {value}");
        }

        private void OnSpent(PlantType type, int value)
        {
            Debug.Log($"Storage {type} spent by {value}");
        }

        private void OnFull(PlantType type)
        {
            Debug.Log($"Storage {type} is full");
        }

        private void OnEmpty(PlantType type)
        {
            Debug.Log($"Storage {type} is empty");
        }

        [Button]
        public void Add(PlantType type, int value)
        {
            bool result = false;
            if (Storages.TryGetStorage(type, out PlantStorage storage))
            {
                result = storage.Add(value);
            }
            
            Debug.Log($"Add to {type} storage value {value}: result - {result}");
        }

        [Button]
        public void Spend(PlantType type, int value)
        {
            bool result = false;
            if (Storages.TryGetStorage(type, out PlantStorage storage))
            {
                result = storage.Spend(value);
            }
            
            Debug.Log($"Spend from {type} storage value {value}: result - {result}");
        }

        [Button]
        public void ResetStorage(PlantType type)
        {
            bool getStorage = Storages.TryGetStorage(type, out PlantStorage storage);
            if (getStorage)
            {
                storage.Reset();
            }
            
            Debug.Log($"Reset {type} storage : result - {getStorage}");
        }
    }
}