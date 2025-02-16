using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Items
{
    public class ItemsCatalog : ScriptableObject  
    {
        [field: SerializeField] 
        public ItemConfig[] Items { get; protected set; }
        
        private readonly Dictionary<string, ItemConfig> _itemsDict = new();

        public bool TryGetItem(string itemName, out ItemConfig itemConfig) => 
            _itemsDict.TryGetValue(itemName, out itemConfig);

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void Awake()
        {
            foreach (ItemConfig settings in Items)
            {
                _itemsDict[settings.Name] = settings;
            }
        }

        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            foreach (ItemConfig settings in Items)
            {
                string itemName = settings.Name;
                _itemsDict[settings.Name] = settings;
                if (itemName is null)
                {
                    Debug.LogWarning($"Item has empty name in catalog");
                    continue;
                }
                
                if (collection.TryAdd(itemName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate item of name {itemName} in catalog");
            }
        }
    }
}