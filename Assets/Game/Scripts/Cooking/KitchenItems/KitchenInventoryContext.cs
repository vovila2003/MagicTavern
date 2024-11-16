using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Cooking
{
    public class KitchenInventoryContext : MonoBehaviour
    {
        [SerializeField] 
        private KitchenItemConfig[] Items;
        
        [SerializeField]
        private KitchenItemsCatalog ItemsCatalog;
        
        private IInventory<KitchenItem> _inventory;

        [ShowInInspector, ReadOnly]
        private List<KitchenItem> KitchenItems => _inventory == null ? new List<KitchenItem>() : _inventory.Items;

        [Inject]
        private void Construct(IInventory<KitchenItem> inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            var items = new KitchenItem[Items.Length];
            for (var i = 0; i < Items.Length; i++)
            {
                items[i] = Items[i].Item.Clone();
            }
            
            _inventory.Setup(items);
        }
        
        [Button]
        public void AddItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig<KitchenItem> itemConfig)) return;
            
            _inventory.AddItem(itemConfig.Item.Clone());
        }
        
        [Button]
        public void AddItemByConfig(KitchenItemConfig itemConfig)
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
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig<KitchenItem> itemConfig)) return;
            
            _inventory.RemoveItem(itemConfig.Item.ItemName);
        }
        
        [Button]
        public void RemoveItemByConfig(KitchenItemConfig itemConfig)
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