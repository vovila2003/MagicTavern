using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class ProductBuyCompleter : ItemBuyCompleter<ProductItem>
    {
        public ProductBuyCompleter(IInventory<ProductItem> inventory) : base(inventory)
        {
        }
    }
}