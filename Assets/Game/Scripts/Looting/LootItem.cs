using System;
using Modules.Items;

namespace Tavern.Looting
{
    [Serializable]
    public class LootItem : Item
    {
        public LootItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();
    
            return new LootItem(Name, Flags, Metadata, attributes);
        }
    }
}