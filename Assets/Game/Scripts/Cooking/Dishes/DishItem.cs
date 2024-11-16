using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item
    {
        public DishItem(string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public new virtual DishItem Clone()
        {
            object[] attributes = GetAttributes();

            return new DishItem(Name, Flags, Metadata, attributes);
        }
    }
}