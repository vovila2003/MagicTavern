using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Looting;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class LootInventorySerializer : BaseInventorySerializer<LootItem>
    {
        public LootInventorySerializer(
            IInventory<LootItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.LootSettings.LootCatalog,
                itemSerializer,
                nameof(LootInventory))
        {
        }
    }
}