using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class PlantProductItem : Item
    {
        public PlantProductItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
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