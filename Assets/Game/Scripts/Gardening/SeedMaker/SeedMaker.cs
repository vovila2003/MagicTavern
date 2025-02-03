using System.Collections.Generic;
using Modules.Gardening;
using Modules.Inventories;
using Sirenix.OdinInspector;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    public class SeedMaker : MonoBehaviour
    {
        private IInventory<PlantProductItem> _productsStorage;
        private SeedInventoryContext _seedsStorage;
        private SeedMakerSettings _settings;
        private readonly Dictionary<string, int> _seeds = new ();
        
        [Inject]
        private void Construct(
            IInventory<PlantProductItem> productsStorage, 
            SeedInventoryContext seedsStorage, 
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
            if (!CanMakeSeeds(type, productCount, out int seedRatio)) return;

            int seedCount = productCount * seedRatio;
            
            _productsStorage.RemoveItems(ProductNameProvider.GetName(type.Name), productCount);
            for (var i = 0; i < seedCount; i++)
            {
                _seedsStorage.AddItemByName(SeedNameProvider.GetName(type.Name));
            }
        }

        private bool CanMakeSeeds(PlantConfig type, int productCount, out int seedRatio)
        {
            seedRatio = 0;

            int itemCount = _productsStorage.GetItemCount(ProductNameProvider.GetName(type.Name));
            if (itemCount < productCount)
            {
                Debug.Log($"Not enough products of type {type}");
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