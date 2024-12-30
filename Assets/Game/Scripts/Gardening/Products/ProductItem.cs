using System;
using Modules.Items;

namespace Tavern.Gardening
{
    [Serializable]
    public class ProductItem : Item
    {
        public ProductItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new ProductItem(Name, Flags, Metadata, attributes);
        }
    }
}