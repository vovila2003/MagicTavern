using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class SeedInventorySerializer : BaseInventorySerializer<SeedItem>
    {
        public SeedInventorySerializer(
            IInventory<SeedItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.SeedCatalog,
                itemSerializer,
                nameof(SeedInventory))
        {
        }
    }
}