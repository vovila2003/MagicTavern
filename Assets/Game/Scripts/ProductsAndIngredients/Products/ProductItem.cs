using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    public abstract class ProductItem : Item
    {
        protected ProductItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra) 
            : base(config, attributes, extra) { }
    }
}