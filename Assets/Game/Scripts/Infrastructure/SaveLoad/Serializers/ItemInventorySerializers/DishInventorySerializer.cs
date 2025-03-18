using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Cooking;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class DishInventorySerializer : BaseInventorySerializer<DishItem>
    {
        public DishInventorySerializer(
            IInventory<DishItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.CookingSettings.DishCatalog,
                itemSerializer,
                nameof(DishInventory))
        {
        }
    }
}