using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Common
{
    public abstract class InventoryContext<T> : MonoBehaviour
        where T : Item
    {
        [SerializeField] 
        private ItemConfig<T>[] Items;
        
        [SerializeField]
        private ItemsCatalog<T> ItemsCatalog;
        
        private IInventory<T> _inventory;

        [ShowInInspector, ReadOnly]
        private List<T> ItemsList => _inventory == null ? new List<T>() : _inventory.Items;

        [Inject]
        private void Construct(IInventory<T> inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            var items = new T[Items.Length];
            for (var i = 0; i < Items.Length; i++)
            {
                items[i] = Items[i].Item.Clone() as T;
            }
            
            _inventory.Setup(items);
        }

        private void OnEnable()
        {
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
            _inventory.OnItemCountChanged += OnItemCountChanged;
        }

        private void OnDisable()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
            _inventory.OnItemCountChanged -= OnItemCountChanged;
        }

        [Button]
        public void AddItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig<T> itemConfig))
            {
                Debug.Log($"{itemName} is not fount in catalog");
                return;
            }
            
            _inventory.AddItem(itemConfig.Item.Clone() as T);
        }

        [Button]
        public void AddItemByConfig(ItemConfig<T> itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            _inventory.AddItem(itemConfig.Item.Clone() as T);
        }

        [Button]
        public void RemoveItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig<T> itemConfig)) return;
            
            _inventory.RemoveItem(itemConfig.Item.ItemName);
        }

        [Button]
        public void RemoveItemByConfig(ItemConfig<T> itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            _inventory.RemoveItem(itemConfig.Item.ItemName);
        }

        private void OnItemAdded(T item)
        {
            Debug.Log($"Item of name {item.ItemName} is added to inventory");
        }

        private void OnItemRemoved(T item)
        {
            Debug.Log($"Item of name {item.ItemName} is removed from inventory");
        }

        private void OnItemCountChanged(T item, int value)
        {
            Debug.Log($"Count of item with name {item.ItemName} is changed to {value}");
        }
    }
}