using UnityEngine;

namespace Modules.Items
{
    public class ItemConfig<T> : ScriptableObject where T : Item
    {
        [SerializeField]
        public T Item;
        
        private void OnValidate() => Item.Attributes ??= new object[] { };
    }
}