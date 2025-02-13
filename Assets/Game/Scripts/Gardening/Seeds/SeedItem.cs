using System;
using Modules.Items;

namespace Tavern.Gardening
{
    [Serializable]
    public class SeedItem : Item
    {
        public SeedItem(string name, ItemFlags flags, Metadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new SeedItem(Name, Flags, Metadata, attributes);
        }
    }
}