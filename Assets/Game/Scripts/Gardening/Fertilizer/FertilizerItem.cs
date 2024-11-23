using System;
using Modules.Inventories;
using Modules.Items;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class FertilizerItem : StackableItem
    {
        public FertilizerItem(string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            object[] attributes = GetAttributes();
    
            return new FertilizerItem(Name, Flags, Metadata, attributes);
        }
    }
}