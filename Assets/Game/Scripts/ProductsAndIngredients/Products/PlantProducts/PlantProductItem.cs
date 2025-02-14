using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class PlantProductItem : ProductItem
    {
        public PlantProductItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new PlantProductItem(Config, GetComponents());
        }
    }
}