using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening.Fertilizer;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class FertilizerItemBuyCompleter : ItemBuyCompleter<FertilizerItem>
    {
        public FertilizerItemBuyCompleter(IInventory<FertilizerItem> inventory) : base(inventory)
        {
        }
    }
}