using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class PlantProductItem : ProductItem
    {
        public PlantProductItem(string name, ItemFlags flags, Metadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new PlantProductItem(Name, Flags, Metadata, attributes);
        }
    }
}