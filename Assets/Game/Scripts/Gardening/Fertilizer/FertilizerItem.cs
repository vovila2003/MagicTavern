using System;
using Modules.Items;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class FertilizerItem : Item
    {
        public FertilizerItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();
    
            return new FertilizerItem(Name, Flags, Metadata, attributes);
        }
    }
}