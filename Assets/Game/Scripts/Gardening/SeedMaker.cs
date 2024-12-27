using System.Collections.Generic;
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
        private ProductInventory _productsStorage;
        private ISeedsStorage _seedsStorage;
        private SeedMakerSettings _settings;
        private readonly Dictionary<string, int> _seeds = new ();
        
        [Inject]
        private void Construct(
            ProductInventory productsStorage, 
            ISeedsStorage seedsStorage, 
            SeedMakerSettings settings)
        {
            _productsStorage = productsStorage;
            _seedsStorage = seedsStorage;
            _settings = settings;
            Initialize();
        }

        [Button]
        public void ShowRatio(PlantConfig type)
        {
            if (!TryGetSeedRatio(type.Name, out int seedRatio))
            {
                Debug.Log($"Convert ratio of type {type.Name} not found");
                return;
            }
            
            Debug.Log($"Convert ratio from {type.Name} to seeds is {seedRatio}");
        }

        [Button]
        public void MakeSeeds(PlantConfig type, int productCount = 1)
        {
            if (!CanMakeSeeds(type, productCount, out PlantStorage seedStorage, out int seedRatio)) return;

            int seedCount = productCount * seedRatio;
            if (seedStorage.LimitType == LimitType.Unlimited)
            {
                _productsStorage.RemoveItems(type.Name, productCount);
                seedStorage.Add(seedCount);
                return;
            }
            
            int availableSeedCount = seedStorage.LimitValue - seedStorage.Value;
            if (availableSeedCount > seedCount)
            {
                _productsStorage.RemoveItems(type.Name, productCount);
                seedStorage.Add(seedCount);
                return;
            }

            int availableProductCount = availableSeedCount / seedRatio;
            availableSeedCount = availableProductCount * seedRatio;
            _productsStorage.RemoveItems(type.Name, availableProductCount);
            seedStorage.Add(availableSeedCount);
        }

        private bool CanMakeSeeds(PlantConfig type, int productCount, 
            out PlantStorage seedStorage, out int seedRatio)
        {
            seedStorage = null;
            seedRatio = 0;

            int itemCount = _productsStorage.GetItemCount(type.Name);
            if (itemCount < productCount)
            {
                Debug.Log($"Not enough products of type {type}");
                return false;
            }
            
            if (!_seedsStorage.TryGetStorage(type.Plant, out seedStorage))
            {
                Debug.Log($"Seed storage of type {type.Name} not found");
                return false;
            }

            if (TryGetSeedRatio(type.Name, out seedRatio)) return true;
            
            Debug.Log($"Convert ratio of type {type.Name} not found");
            return false;
        }

        private bool TryGetSeedRatio(string plant, out int convertRatio)
        {
            bool contains = _seeds.TryGetValue(plant, out int ratio);
            convertRatio = ratio;
            return contains;
        }

        private void Initialize()
        {
            foreach (SeedParams settings in _settings.Params)
            {
                string plant = settings.Type.PlantName;
                _seeds[plant] = settings.Ratio;
            }
        }
    }
}