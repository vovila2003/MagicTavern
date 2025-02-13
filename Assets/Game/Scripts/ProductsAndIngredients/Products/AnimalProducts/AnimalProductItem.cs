using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class AnimalProductItem : ProductItem
    {
        public AnimalProductItem(string name, ItemFlags flags, Metadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new AnimalProductItem(Name, Flags, Metadata, attributes);
        }
    }
}