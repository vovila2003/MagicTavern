using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class AnimalProductInventorySerializer : BaseInventorySerializer<AnimalProductItem>
    {
        public AnimalProductInventorySerializer(
            IInventory<AnimalProductItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.AnimalProductCatalog,
                itemSerializer,
                nameof(AnimalProductInventory))
        {
        }
    }
}