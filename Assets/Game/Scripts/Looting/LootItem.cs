using System;
using Modules.Items;

namespace Tavern.Looting
{
    [Serializable]
    public class LootItem : Item
    {
        public LootItem(string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public new virtual LootItem Clone()
        {
            object[] attributes = GetAttributes();
    
            return new LootItem(Name, Flags, Metadata, attributes);
        }
    }
}