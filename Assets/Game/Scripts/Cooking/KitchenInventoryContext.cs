using System.Collections.Generic;
using Modules.Cooking;
using Modules.Inventories;
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
        
        private ListInventory<KitchenItem> _inventory;

        [ShowInInspector, ReadOnly]
        private List<KitchenItem> KitchenItems => _inventory == null ? new List<KitchenItem>() : _inventory.Items;

        [Inject]
        private void Construct(ListInventory<KitchenItem> inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            var items = new KitchenItem[Items.Length];
            for (var i = 0; i < Items.Length; i++)
            {
                items[i] = Items[i].KitchenItem.Clone();
            }
            
            _inventory.Setup(items);
        }
        
        [Button]
        public void AddItem(KitchenItemType type)
        {
            if (!ItemsCatalog.TryGetKitchenItem(type, out KitchenItemConfig itemConfig)) return;
            
            _inventory.AddItem(itemConfig.KitchenItem.Clone());
        }

        [Button]
        public void RemoveItem(KitchenItemType type)
        {
            if (!ItemsCatalog.TryGetKitchenItem(type, out KitchenItemConfig itemConfig)) return;
            
            _inventory.RemoveItem(itemConfig.KitchenItem.ItemName);
        }
    }
}