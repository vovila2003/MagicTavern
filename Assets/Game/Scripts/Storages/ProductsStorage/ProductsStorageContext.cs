using Modules.Gardening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class ProductsStorageContext : MonoBehaviour
    {
        [SerializeField] 
        private bool DebugMode;

        [SerializeField, ShowIf("DebugMode")] 
        private int StartValueInStorageInDebugMode;
        
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

        private void Start()
        {
            if (!DebugMode) return;

            foreach (PlantStorage storage in Storages.PlantStorages)
            {
                storage.Add(StartValueInStorageInDebugMode);
            }
        }

        private void ValueAdded(Plant type, int value)
        {
            Debug.Log($"Storage {type.PlantName} added by {value}");
        }

        private void ValueChanged(Plant type, int value)
        {
            Debug.Log($"Storage {type.PlantName} value changed to {value}");
        }

        private void OnSpent(Plant type, int value)
        {
            Debug.Log($"Storage {type.PlantName} spent by {value}");
        }

        private void OnFull(Plant type)
        {
            Debug.Log($"Storage {type.PlantName} is full");
        }

        private void OnEmpty(Plant type)
        {
            Debug.Log($"Storage {type.PlantName} is empty");
        }

        [Button]
        public void Add(PlantConfig plant, int value)
        {
            bool result = false;
            if (Storages.TryGetStorage(plant.Plant, out PlantStorage storage))
            {
                result = storage.Add(value);
            }
            
            Debug.Log($"Add to {plant.Name} storage value {value}: result - {result}");
        }

        [Button]
        public void Spend(PlantConfig plant, int value)
        {
            bool result = false;
            if (Storages.TryGetStorage(plant.Plant, out PlantStorage storage))
            {
                result = storage.Spend(value);
            }
            
            Debug.Log($"Spend from {plant.Name} storage value {value}: result - {result}");
        }

        [Button]
        public void ResetStorage(PlantConfig plant)
        {
            bool getStorage = Storages.TryGetStorage(plant.Plant, out PlantStorage storage);
            if (getStorage)
            {
                storage.Reset();
            }
            
            Debug.Log($"Reset {plant.Name} storage : result - {getStorage}");
        }
    }
}