using System;
using Modules.Items;
using Sirenix.OdinInspector;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item, IExtraItem
    {
        [ShowInInspector, ReadOnly]
        public bool IsExtra { get; set; }
        
        public DishItem(string name, ItemFlags flags, ItemMetadata metadata, params IItemComponent[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            IItemComponent[] attributes = GetComponents();

            return new DishItem(Name, Flags, Metadata, attributes);
        }
    }
}