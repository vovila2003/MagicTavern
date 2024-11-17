using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class KitchenItem : Item
    {
        public KitchenItem(string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            object[] attributes = GetAttributes();

            return new KitchenItem(Name, Flags, Metadata, attributes);
        }
    }
}