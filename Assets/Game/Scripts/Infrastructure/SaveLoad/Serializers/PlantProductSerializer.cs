using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class PlantProductSerializer : InventorySerializer<PlantProductItem>
    {
        public PlantProductSerializer(IInventory<PlantProductItem> inventory, GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.PlantProductCatalog,
                "PlantProductInventory")
        {
        }
    }
}