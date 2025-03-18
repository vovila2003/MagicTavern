using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class AnimalProductItem : ProductItem
    {
        public AnimalProductItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra) 
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new AnimalProductItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}