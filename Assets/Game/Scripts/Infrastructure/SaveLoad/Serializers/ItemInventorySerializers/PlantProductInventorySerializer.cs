using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class PlantProductInventorySerializer : BaseInventorySerializer<PlantProductItem>
    {
        public PlantProductInventorySerializer(
            IInventory<PlantProductItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.PlantProductCatalog,
                itemSerializer,
                nameof(PlantProductInventory))
        {
        }
    }
}