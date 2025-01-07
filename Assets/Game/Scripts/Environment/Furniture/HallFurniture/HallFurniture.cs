using System;
using Modules.Items;

namespace Tavern.Environment.Furniture
{
    [Serializable]
    public class HallFurniture : FurnitureItem
    {
        protected HallFurniture(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();
        
            return new HallFurniture(Name, Flags, Metadata, attributes);
        }
    }
}