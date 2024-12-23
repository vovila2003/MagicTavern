using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Cooking;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class DishItemBuyCompleter : ItemBuyCompleter<DishItem>
    {
        public DishItemBuyCompleter(IInventory<DishItem> inventory) : base(inventory)
        {
        }
    }
}