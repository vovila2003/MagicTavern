using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening.Medicine;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class MedicineInventorySerializer : BaseInventorySerializer<MedicineItem>
    {
        public MedicineInventorySerializer(
            IInventory<MedicineItem> inventory,
            ItemSerializer itemSerializer,
            GameSettings settings)
            : base(inventory, 
                settings.GardeningSettings.MedicineCatalog,
                itemSerializer,
                nameof(MedicineInventory))
        {
        }
    }
}