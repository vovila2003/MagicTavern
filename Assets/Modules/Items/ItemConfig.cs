using System.Collections.Generic;
using UnityEngine;

namespace Modules.Items
{
    public abstract class ItemConfig<T> : ScriptableObject where T : Item
    {
        [SerializeField] 
        private T Item;
        
        [SerializeField]
        public ItemMetadata Metadata;

        public T GetItem()
        {
            Item.Metadata = Metadata;
            return Item;
        }

        protected virtual void Awake()
        {
            Item.Components ??= new List<IItemComponent>();
        }
    }
}