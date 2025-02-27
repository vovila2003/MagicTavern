using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    public abstract class ProductItem : Item
    {
        protected ProductItem(ItemConfig config, params IItemComponent[] attributes) 
            : base(config, attributes) { }
    }
}