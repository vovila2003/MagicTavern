using System.Collections.Generic;
using Modules.Items;
using Modules.Looting;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Looting
{
    public class LootInventoryContext : MonoBehaviour
    {
        [SerializeField] 
        private LootItemConfig[] Items;
        
        [SerializeField]
        private LootItemsCatalog ItemsCatalog;
        
        private LootInventory _inventory;

        [ShowInInspector, ReadOnly]
        private List<LootItem> LootItems => _inventory == null ? new List<LootItem>() : _inventory.Items;

        [Inject]
        private void Construct(LootInventory inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            var items = new LootItem[Items.Length];
            for (var i = 0; i < Items.Length; i++)
            {
                items[i] = Items[i].Item.Clone();
            }
            
            _inventory.Setup(items);
        }
        
        [Button]
        public void AddItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig<LootItem> itemConfig)) return;
            
            _inventory.AddItem(itemConfig.Item.Clone());
        }
        
        [Button]
        public void AddItemByConfig(LootItemConfig itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            _inventory.AddItem(itemConfig.Item.Clone());
        }

        [Button]
        public void RemoveItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig<LootItem> itemConfig)) return;
            
            _inventory.RemoveItem(itemConfig.Item.ItemName);
        }
        
        [Button]
        public void RemoveItemByConfig(LootItemConfig itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            _inventory.RemoveItem(itemConfig.Item.ItemName);
        }
    }
}