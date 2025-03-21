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

        public IInventory<T> Inventory {get; private set;}

        [ShowInInspector, ReadOnly]
        private List<T> ItemsList => Inventory == null ? new List<T>() : Inventory.Items;
        
        [Inject]
        private void Construct(IInventory<T> inventory)
        {
            Inventory = inventory;
        }

        private void Awake()
        {
            var items = new T[Items.Length];
            for (var i = 0; i < Items.Length; i++)
            {
                items[i] = Items[i].Create() as T;
            }
            
            Inventory.Setup(items);

            if (!DebugMode) return;
            
            foreach (ItemConfig config in ItemsCatalog.Items)
            {
                for (var i = 0; i < Count; ++i)
                {
                    Inventory.AddItem(config.Create() as T);    
                }
            }
        }

        private void OnEnable()
        {
            Inventory.OnItemAdded += OnItemAdded;
            Inventory.OnItemRemoved += OnItemRemoved;
            Inventory.OnItemCountChanged += OnItemCountChanged;
        }

        private void OnDisable()
        {
            Inventory.OnItemAdded -= OnItemAdded;
            Inventory.OnItemRemoved -= OnItemRemoved;
            Inventory.OnItemCountChanged -= OnItemCountChanged;
        }

        [Button]
        public void AddItemByName(string itemName, int value = 1)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig itemConfig))
            {
                Debug.Log($"{itemName} is not fount in catalog");
                return;
            }
            
            Inventory.AddItems(itemConfig.Create() as T, value);
        }

        [Button]
        public void AddItemByConfig(ItemConfig itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            Inventory.AddItem(itemConfig.Create() as T);
        }

        [Button]
        public void RemoveItemByName(string itemName)
        {
            if (!ItemsCatalog.TryGetItem(itemName, out ItemConfig itemConfig)) return;
            
            Inventory.RemoveItem(itemConfig.Name);
        }

        [Button]
        public void RemoveItemByConfig(ItemConfig itemConfig)
        {
            if (itemConfig is null) 
            {
                Debug.LogError($"{nameof(itemConfig)} is null");
                return;
            }
            
            Inventory.RemoveItem(itemConfig.Name);
        }

        private void OnItemAdded(Item item, IInventoryBase inventory)
        {
            Debug.Log($"Item of name {item.ItemName} is added to inventory");
        }

        private void OnItemRemoved(Item item, IInventoryBase inventory)
        {
            Debug.Log($"Item of name {item.ItemName} is removed from inventory");
        }

        private void OnItemCountChanged(Item item, int value)
        {
            Debug.Log($"Count of item with name {item.ItemName} is changed to {value}");
        }
    }
}