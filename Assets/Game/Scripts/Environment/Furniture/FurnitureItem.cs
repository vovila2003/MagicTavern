using System;
using Modules.Items;

namespace Tavern.Environment.Furniture
{
    [Serializable]
    public abstract class FurnitureItem : Item
    {
        protected FurnitureItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
    }
}