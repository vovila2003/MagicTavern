using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.Gardening
{
    [UsedImplicitly]
    public class SeedMaker
    {
        private readonly IInventory<PlantProductItem> _productsStorage;
        private readonly IInventory<SeedItem> _seedsStorage;
        private readonly SeedCatalog _seedCatalog;

        private SeedMaker(
            IInventory<PlantProductItem> productsStorage, 
            IInventory<SeedItem> seedsStorage,
            GameSettings settings)
        {
            _productsStorage = productsStorage;
            _seedsStorage = seedsStorage;
            _seedCatalog = settings.GardeningSettings.SeedCatalog;
        }

        public void MakeSeeds(PlantProductItem item, int productCount = 1)
        {
            int itemCount = _productsStorage.GetItemCount(item.ItemName);
            if (itemCount < productCount) return;

            if (!item.TryGet(out ComponentPlant componentPlant)) return;

            string seedName = SeedNameProvider.GetName(componentPlant.Config.Plant.PlantName);
            if (!_seedCatalog.TryGetItem(seedName, out ItemConfig itemConfig))
            {
                Debug.Log($"Seed with name {seedName} is not found in seed catalog");
                return;
            }

            if (itemConfig is not SeedItemConfig seedItemConfig) return;

            if (!item.TryGet(out ComponentProductToSeedRatio ratio)) return;

            int seedCount = productCount * ratio.ProductToSeedRatio;

            _productsStorage.RemoveItems(item, productCount);
            _seedsStorage.AddItems(seedItemConfig.Create(), seedCount);
        }
    }
}