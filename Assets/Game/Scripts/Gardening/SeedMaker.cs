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
        private void Construct(IProductsStorage productsStorage, ISeedsStorage seedsStorage, SeedMakerSettings settings)
        {
            _productsStorage = productsStorage;
            _seedsStorage = seedsStorage;
            _settings = settings;
        }

        [Button]
        public void ShowRatio(PlantType type)
        {
            if (!_settings.TryGetSeedRatio(type, out int seedRatio))
            {
                Debug.Log($"Convert ratio of type {type} not found");
                _ratio = 0;
                return;
            }
            
            _ratio = seedRatio;
        }

        [Button]
        public void MakeSeeds(PlantType type, int productCount)
        {
            if (!_productsStorage.TryGetStorage(type, out PlantStorage plantStorage))
            {
                Debug.Log($"Product storage of type {type} not found");
                return;
            }

            if (!plantStorage.CanSpend(productCount))
            {
                Debug.Log($"Not enough products of type {type}");
                return;
            }
            
            if (!_seedsStorage.TryGetStorage(type, out PlantStorage seedStorage))
            {
                Debug.Log($"Seed storage of type {type} not found");
                return;
            }

            if (!_settings.TryGetSeedRatio(type, out int seedRatio))
            {
                Debug.Log($"Convert ratio of type {type} not found");
                return;
            }
            
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
    }
}