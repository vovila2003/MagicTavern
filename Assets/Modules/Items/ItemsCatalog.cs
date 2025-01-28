using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Items
{
    public class ItemsCatalog<T> : ScriptableObject where T : Item 
    {
        [SerializeField] 
        protected ItemConfig<T>[] Items;
        
        private readonly Dictionary<string, ItemConfig<T>> _itemsDict = new();

        public bool TryGetItem(string itemName, out ItemConfig<T> itemConfig) => 
            _itemsDict.TryGetValue(itemName, out itemConfig);

        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            foreach (ItemConfig<T> settings in Items)
            {
                string itemName = settings.GetItem().ItemName;
                _itemsDict[itemName] = settings;
                if (collection.TryAdd(itemName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate item of name {itemName} in catalog");
            }            
        }
    }
}