using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class AnimalProductSerializer : InventorySerializer<AnimalProductItem>
    {
        public AnimalProductSerializer(IInventory<AnimalProductItem> inventory, GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.AnimalProductCatalog,
                "AnimalProductInventory")
        {
        }
    }
}