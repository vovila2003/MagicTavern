using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class KitchenItem : Item
    {
        public KitchenItem(string name, ItemFlags flags, Metadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new KitchenItem(Name, Flags, Metadata, attributes);
        }
    }
}