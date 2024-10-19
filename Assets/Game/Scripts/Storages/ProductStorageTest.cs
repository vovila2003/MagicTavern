using Modules.Products.Plants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class ProductStorageTest : MonoBehaviour
    {
        [SerializeField]
        private ProductsStorage Storages;

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