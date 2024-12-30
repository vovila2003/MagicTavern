using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class SeedBuyCompleter : ItemBuyCompleter<SeedItem>
    {
        public SeedBuyCompleter(IInventory<SeedItem> inventory) : base(inventory)
        {
        }
    }
}