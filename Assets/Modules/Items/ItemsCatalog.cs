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

        public virtual string CatalogName { get; private set; }

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
                if (itemName is null)
                {
                    Debug.LogWarning($"Item has empty name in catalog {CatalogName}");
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