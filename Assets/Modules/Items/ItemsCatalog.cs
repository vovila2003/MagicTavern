using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Items
{
    public class ItemsCatalog : ScriptableObject, IItemsCatalog
    {
        [field: SerializeField] 
        public List<ItemConfig> Items { get; protected set; } = new();

        protected readonly Dictionary<string, ItemConfig> ItemsDict = new();

        public virtual string CatalogName { get; private set; }

        public bool TryGetItem(string itemName, out ItemConfig itemConfig)
        {
            if (itemName is not null) return ItemsDict.TryGetValue(itemName, out itemConfig);
            
            itemConfig = null;
            return false;
        }

        public void AddConfig(ItemConfig config)
        {
            Items.Add(config);
            ItemsDict.Add(config.Name, config);
        }
        
        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void Awake()
        {
            ItemsDict.Clear();
            foreach (ItemConfig settings in Items)
            {
                if (settings?.Name != null)
                {
                    ItemsDict.Add(settings.Name, settings);
                }
            }
        }

        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            ItemsDict.Clear();
            foreach (ItemConfig settings in Items)
            {
                string itemName = settings.Name;
                if (itemName is null)
                {
                    Debug.LogWarning($"Item has empty name in catalog {CatalogName}");
                    continue;
                }
                ItemsDict.Add(settings.Name, settings);
                
                if (collection.TryAdd(itemName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate item of name {itemName} in catalog");
            }
        }
    }
}