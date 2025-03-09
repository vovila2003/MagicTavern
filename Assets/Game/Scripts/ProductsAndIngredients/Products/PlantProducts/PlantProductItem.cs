using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class PlantProductItem : ProductItem
    {
        public PlantProductItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra) 
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new PlantProductItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}