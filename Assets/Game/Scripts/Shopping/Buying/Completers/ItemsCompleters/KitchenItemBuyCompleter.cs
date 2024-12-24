using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Cooking;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class KitchenItemBuyCompleter : ItemBuyCompleter<KitchenItem>
    {
        public KitchenItemBuyCompleter(IInventory<KitchenItem> inventory) : base(inventory)
        {
        }
    }
}