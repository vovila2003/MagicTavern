using Modules.Gardening;
using Modules.Storages;
using Sirenix.OdinInspector;
using Tavern.Settings;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    public class SeedMaker : MonoBehaviour
    {
        private IProductsStorage _productsStorage;
        private ISeedsStorage _seedsStorage;
        private SeedMakerSettings _settings;
        
        [ShowInInspector, ReadOnly]
        private float _ratio;
        
        [Inject]
        private void Construct(
            IProductsStorage productsStorage, 
            ISeedsStorage seedsStorage, 
            SeedMakerSettings settings)
        {
            _productsStorage = productsStorage;
            _seedsStorage = seedsStorage;
            _settings = settings;
        }

        [Button]
        public void ShowRatio(PlantConfig type)
        {
            if (!_settings.TryGetSeedRatio(type.Plant, out int seedRatio))
            {
                Debug.Log($"Convert ratio of type {type.Name} not found");
                _ratio = 0;
                return;
            }
            
            _ratio = seedRatio;
        }

        [Button]
        public void MakeSeeds(PlantConfig type, int productCount = 1)
        {
            if (!CanMakeSeeds(type, productCount, out PlantStorage plantStorage, 
                    out PlantStorage seedStorage, out int seedRatio)) return;

            int seedCount = productCount * seedRatio;
            if (seedStorage.LimitType == LimitType.Unlimited)
            {
                plantStorage.Spend(productCount);
                seedStorage.Add(seedCount);
                return;
            }
            
            int availableSeedCount = seedStorage.LimitValue - seedStorage.Value;
            if (availableSeedCount > seedCount)
            {
                plantStorage.Spend(productCount);
                seedStorage.Add(seedCount);
                return;
            }

            int availableProductCount = availableSeedCount / seedRatio;
            availableSeedCount = availableProductCount * seedRatio;
            plantStorage.Spend(availableProductCount);
            seedStorage.Add(availableSeedCount);
        }

        private bool CanMakeSeeds(PlantConfig type, int productCount, out PlantStorage plantStorage,
            out PlantStorage seedStorage, out int seedRatio)
        {
            plantStorage = null;
            seedStorage = null;
            seedRatio = 0;
            if (!_productsStorage.TryGetStorage(type.Plant, out plantStorage))
            {
                Debug.Log($"Product storage of type {type.Name} not found");
                return false;
            }

            if (!plantStorage.CanSpend(productCount))
            {
                Debug.Log($"Not enough products of type {type}");
                return false;
            }
            
            if (!_seedsStorage.TryGetStorage(type.Plant, out seedStorage))
            {
                Debug.Log($"Seed storage of type {type.Name} not found");
                return false;
            }

            if (_settings.TryGetSeedRatio(type.Plant, out seedRatio)) return true;
            
            Debug.Log($"Convert ratio of type {type.Name} not found");
            return false;
        }
    }
}