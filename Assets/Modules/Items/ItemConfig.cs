using System.Collections.Generic;
using UnityEngine;

namespace Modules.Items
{
    public abstract class ItemConfig<T> : ScriptableObject where T : Item
    {
        [SerializeField]
        public T Item;

        protected virtual void Awake()
        {
            Item.Components ??= new List<IItemComponent>();
        }
    }
}