using System.Collections.Generic;
using UnityEngine;

namespace Modules.Items
{
    public abstract class ItemConfig<T> : ScriptableObject where T : Item
    {
        [SerializeField]
        public T Item;
        
        [SerializeField]
        public ItemMetadata Metadata;

        protected virtual void Awake()
        {
            Item.Components ??= new List<IItemComponent>();
            Item.Metadata = Metadata;
        }

        protected virtual void OnValidate()
        {
            Item.Metadata = Metadata;
        }
    }
}