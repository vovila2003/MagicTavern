using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;
using Tavern.ProductsAndIngredients;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public sealed class ProductBuyCompleter : ItemBuyCompleter<PlantProductItem>
    {
        public ProductBuyCompleter(IInventory<PlantProductItem> inventory) : base(inventory)
        {
        }
    }
}