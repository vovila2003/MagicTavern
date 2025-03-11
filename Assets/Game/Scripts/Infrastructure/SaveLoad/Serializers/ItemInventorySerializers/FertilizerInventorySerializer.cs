using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening.Fertilizer;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class FertilizerInventorySerializer : BaseInventorySerializer<FertilizerItem>
    {
        public FertilizerInventorySerializer(
            IInventory<FertilizerItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.FertilizerCatalog,
                itemSerializer,
                nameof(FertilizerInventory))
        {
        }
    }
}