using System;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class MedicineItem : Item
    {
        public MedicineItem(string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            object[] attributes = GetAttributes();
    
            return new MedicineItem(Name, Flags, Metadata, attributes);
        }
    }
}