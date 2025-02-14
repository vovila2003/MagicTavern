using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Items
{
    [CreateAssetMenu(
        fileName = "ItemsCatalog", 
        menuName = "Settings/Items/Items Catalog")]
    public class ItemsCatalog : ScriptableObject  
    {
        [SerializeField] 
        protected ItemConfig[] Items;
        
        private readonly Dictionary<string, ItemConfig> _itemsDict = new();

        public bool TryGetItem(string itemName, out ItemConfig itemConfig) => 
            _itemsDict.TryGetValue(itemName, out itemConfig);

        public IReadOnlyCollection<ItemConfig> AllItems => _itemsDict.Values;

        [Button]
        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            foreach (ItemConfig settings in Items)
            {
                string itemName = settings.Name;
                if (itemName is null)
                {
                    Debug.LogWarning($"Item has empty name in catalog");
                    continue;
                }
                
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