using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Tavern.Common
{
    public abstract class InventoryContext<T> : MonoBehaviour
        where T : Item
    {
        [FormerlySerializedAs("Items")] [SerializeField] 
        private ItemConfig[] ItemsConfigs;
        
        [SerializeField]
        private ItemsCatalog ItemsCatalog;
        
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
            var items = new T[ItemsConfigs.Length];
            for (var i = 0; i < ItemsConfigs.Length; i++)
            {
                items[i] = ItemsConfigs[i].Create() as T;
            }
            
            _inventory.Setup(items);
        }

        private void OnEnable()
        {
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
        }

        private void OnDisable()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
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

        private void OnItemAdded(T item)
        {
            Debug.Log($"Item of name {item.ItemName} is added to inventory");
        }

        private void OnItemRemoved(T item)
        {
            Debug.Log($"Item of name {item.ItemName} is removed from inventory");
        }
    }
}