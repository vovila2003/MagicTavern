using UnityEngine;

namespace Modules.Items
{
    public class ItemConfig<T> : ScriptableObject where T : Item
    {
        [SerializeField]
        public T Item;

        protected virtual void OnValidate()
        {
            Item.Attributes ??= new object[]{};
        }
    }
}