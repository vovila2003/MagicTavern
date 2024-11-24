using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item
    {
        public DishItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new DishItem(Name, Flags, Metadata, attributes);
        }
    }
}