using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Looting;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class LootItemBuyCompleter : ItemBuyCompleter<LootItem>
    {
        public LootItemBuyCompleter(IInventory<LootItem> inventory) : base(inventory)
        {
        }
    }
}