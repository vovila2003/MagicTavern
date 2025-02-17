using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Common
{
    public abstract class StackableInventoryContext<T> : MonoBehaviour
        where T : Item
    {
        [SerializeField] 
        private ItemConfig[] Items;
        
        [SerializeField]
        private ItemsCatalog ItemsCatalog;

        [SerializeField] 
        private bool DebugMode;

        [SerializeField, ShowIf("DebugMode")] 
        private int Count;
        
        private IStackableInventory<T> _inventory;

        [ShowInInspector, ReadOnly]
        private List<T> ItemsList => _inventory == null ? new List<T>() : _inventory.Items;

        [Inject]
        private void Construct(IStackableInventory<T> inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            var items = new T[Items.Length];
            for (var i = 0; i < Items.Length; i++)
            {
                items[i] = Items[i].Create() as T;
            }
            
            _inventory.Setup(items);

            if (!DebugMode) return;
            
            foreach (ItemConfig config in ItemsCatalog.Items)
            {
                for (var i = 0; i < Count; ++i)
                {
                    _inventory.AddItem(config.Create() as T);    
                }
            }
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
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig itemConfig))
            {
                Debug.Log($"{itemName} is not fount in catalog");
                return;
            }
            
            _inventory.AddItem(itemConfig.Create() as T);
        }

        [Button]
        public void AddItemByConfig(ItemConfig itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            _inventory.AddItem(itemConfig.Create() as T);
        }

        [Button]
        public void RemoveItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig itemConfig)) return;
            
            _inventory.RemoveItem(itemConfig.Name);
        }

        [Button]
        public void RemoveItemByConfig(ItemConfig itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            _inventory.RemoveItem(itemConfig.Name);
        }

        private void OnItemAdded(Item item, IInventoryBase inventory)
        {
            Debug.Log($"Item of name {item.ItemName} is added to inventory");
        }

        private void OnItemRemoved(Item item, IInventoryBase inventory)
        {
            Debug.Log($"Item of name {item.ItemName} is removed from inventory");
        }

        private void OnItemCountChanged(T item, int value)
        {
            Debug.Log($"Count of item with name {item.ItemName} is changed to {value}");
        }
    }
}