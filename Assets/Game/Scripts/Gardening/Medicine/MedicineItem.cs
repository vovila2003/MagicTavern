using System;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class MedicineItem : Item
    {
        public MedicineItem(string name, ItemFlags flags, Metadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();
    
            return new MedicineItem(Name, Flags, Metadata, attributes);
        }
    }
}