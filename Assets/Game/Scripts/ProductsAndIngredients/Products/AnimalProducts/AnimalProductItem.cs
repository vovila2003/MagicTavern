using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class AnimalProductItem : ProductItem
    {
        public AnimalProductItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new AnimalProductItem(Config, GetComponents());
        }
    }
}