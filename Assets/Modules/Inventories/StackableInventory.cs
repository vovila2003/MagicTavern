using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Items;

namespace Modules.Inventories
{
    public sealed class StackableInventory<T> where T : Item
    {
        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;
        
        private readonly ListInventory<T> _inventory;

        public StackableInventory(ListInventory<T> inventory)
        {
            _inventory = inventory;
        }

        public void Setup(params T[] items)
        {
            _inventory.Setup(items);
        }

        public void AddItem(T item)
        {
            if (IsStackable(item))
            {
                AddStackable(item);
            }
            else
            {
                AddSingle(item);
            }
        }

        public void RemoveItem(T item)
        {
            if (!IsItemExists(item)) return;
            
            if (IsStackable(item))
            {
                RemoveStackable(item);
            }
            else
            {
                RemoveSingle(item);
            }
        }

        public T RemoveItem(string name)
        {
            if (!FindAllItems(name, out List<T> items)) return null;
            
            T lastItem = items.Last();
            if (IsStackable(lastItem))
            {
                T item = RemoveItemWithMinValue(items);
                return item;
            }

            _inventory.RemoveItem(lastItem);
            OnItemRemoved?.Invoke(lastItem);
            
            return lastItem;
        }

        public void RemoveItems(string name, int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                RemoveItem(name);
            }
        }

        public IReadOnlyList<T> GetItems() => _inventory.GetItems();

        public int GetItemCount(string name)
        {
            if (!FindAllItems(name, out List<T> items)) return 0;

            return IsStackable(items.First())
                ? items.Sum(item => item.GetAttribute<AttributeStackable>().Value)
                : items.Count;
        }

        public bool FindItem(string name, out T item)
        {
            bool result = _inventory.FindItem(name, out T inventoryItem);
            item = inventoryItem;
            
            return result;
        }

        public bool FindAllItems(string name, out List<T> items)
        {
            bool result = _inventory.FindAllItems(name, out List<T> inventoryItems);
            items = inventoryItems;
            
            return result;
        }

        public bool IsItemExists(T item) => 
            IsStackable(item) ? FindItem(item.ItemName, out T _) : _inventory.IsItemExists(item);

        private bool IsStackable(T item) => item.ItemFlags.HasFlag(ItemFlags.STACKABLE);

        private void AddSingle(T item)
        {
            if (IsItemExists(item)) return;
            
            _inventory.AddItem(item);
            OnItemAdded?.Invoke(item);
        }

        private void AddStackable(T item)
        {
            if (FindAllItems(item.ItemName, out List<T> existsItems))
            {
                if (existsItems.Any(TryIncrementStackable)) return;
            }
            
            _inventory.AddItem(item);
            TryIncrementStackable(item);
            OnItemAdded?.Invoke(item);
        }

        private static bool TryIncrementStackable(T existsItem)
        {
            var componentStackable = existsItem.GetAttribute<AttributeStackable>();
            if (componentStackable.IsFull) return false;
            
            componentStackable.Value++;
            
            return true;
        }

        private void RemoveStackable(T item)
        {
            if (TryDecrementStackable(item)) return;
            
            RemoveSingle(item);
        }

        private void RemoveSingle(T item)
        {
            _inventory.RemoveItem(item);
            OnItemRemoved?.Invoke(item);
        }

        private bool TryDecrementStackable(T item)
        {
            var stackableComponent = item.GetAttribute<AttributeStackable>();
            if (stackableComponent.Value <= 1) return false;
            
            stackableComponent.Value--;
            
            return true;
        }

        private T RemoveItemWithMinValue(IReadOnlyList<T> items)
        {
            T itemWithMinValue = items[0];
            int minValue = itemWithMinValue.GetAttribute<AttributeStackable>().Value;
            for (var i = 1; i < items.Count; i++)
            {
                T item = items[i];
                int value = item.GetAttribute<AttributeStackable>().Value;
                if (value > minValue) continue;
                
                minValue = value;
                itemWithMinValue = item;
            }
            RemoveStackable(itemWithMinValue);
            
            return itemWithMinValue;
        }
    }
}