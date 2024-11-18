using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item
    {
        [SerializeField, Space] 
        private int Exotic;
        
        public int DishExotic => Exotic;
        
        public DishItem(string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
            : base(name, flags, metadata, attributes)
        {
        }
        
        public override Item Clone()
        {
            object[] attributes = GetAttributes();

            return new DishItem(Name, Flags, Metadata, attributes);
        }
    }
}