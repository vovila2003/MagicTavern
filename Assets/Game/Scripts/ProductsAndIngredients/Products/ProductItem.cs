using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    public abstract class ProductItem : Item
    {
        protected ProductItem(string name, ItemFlags flags, Metadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
    }
}