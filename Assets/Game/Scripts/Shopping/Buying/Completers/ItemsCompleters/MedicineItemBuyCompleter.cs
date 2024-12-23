using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening.Medicine;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class MedicineItemBuyCompleter : ItemBuyCompleter<MedicineItem>
    {
        public MedicineItemBuyCompleter(IInventory<MedicineItem> inventory) : base(inventory)
        {
        }
    }
}