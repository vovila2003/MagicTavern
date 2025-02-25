using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Items;

namespace Modules.Inventories
{
    public class StackableInventory<T> : IStackableInventory<T> where T : Item
    {
        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;
        public event Action<T, int> OnItemCountChanged;
        
        private readonly ListInventory<T> _listInventory;

        public List<T> Items => _listInventory.Items;

        protected StackableInventory()
        {
            _listInventory = new ListInventory<T>();
        }

        protected StackableInventory(ListInventory<T> listInventory)
        {
            _listInventory = listInventory;
        }

        public void Setup(params T[] items)
        {
            foreach (T item in items)
            {
                AddItem(item);                
            }
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

            _listInventory.RemoveItem(lastItem);
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

        public IReadOnlyList<T> GetItems() => _listInventory.GetItems();

        public int GetItemCount(string name)
        {
            if (!FindAllItems(name, out List<T> items)) return 0;

            return IsStackable(items.First())
                ? items.Sum(item => item.Get<ComponentStackable>().Value)
                : items.Count;
        }

        public bool FindItem(string name, out T item)
        {
            bool result = _listInventory.FindItem(name, out T inventoryItem);
            item = inventoryItem;
            
            return result;
        }

        public bool FindAllItems(string name, out List<T> items)
        {
            bool result = _listInventory.FindAllItems(name, out List<T> inventoryItems);
            items = inventoryItems;
            
            return result;
        }

        public bool IsItemExists(T item) => 
            IsStackable(item) ? FindItem(item.ItemName, out T _) : _listInventory.IsItemExists(item);

        public bool IsItemExists(string name) => FindItem(name, out T _);

        private bool IsStackable(T item) => item.ItemFlags.HasFlag(ItemFlags.Stackable);

        private void AddSingle(T item)
        {
            if (IsItemExists(item)) return;
            
            _listInventory.AddItem(item);
            OnItemAdded?.Invoke(item);
        }

        private void AddStackable(T item)
        {
            if (FindAllItems(item.ItemName, out List<T> existsItems))
            {
                if (existsItems.Any(TryIncrementStackable)) return;
            }
            
            _listInventory.AddItem(item);
            OnItemAdded?.Invoke(item);
            OnItemCountChanged?.Invoke(item, item.Get<ComponentStackable>().Value);
        }

        private bool TryIncrementStackable(T existsItem)
        {
            var stackableComponent = existsItem.Get<ComponentStackable>();
            if (stackableComponent.IsFull) return false;
            
            stackableComponent.Value++;
            OnItemCountChanged?.Invoke(existsItem, stackableComponent.Value);
            
            return true;
        }

        private void RemoveStackable(T item)
        {
            if (TryDecrementStackable(item)) return;
            
            RemoveSingle(item);
        }

        private void RemoveSingle(T item)
        {
            _listInventory.RemoveItem(item);
            OnItemRemoved?.Invoke(item);
        }

        private bool TryDecrementStackable(T item)
        {
            var stackableComponent = item.Get<ComponentStackable>();
            if (stackableComponent.Value <= 1) return false;
            
            stackableComponent.Value--;
            OnItemCountChanged?.Invoke(item, stackableComponent.Value);
            
            return true;
        }

        private T RemoveItemWithMinValue(IReadOnlyList<T> items)
        {
            T itemWithMinValue = items[0];
            int minValue = itemWithMinValue.Get<ComponentStackable>().Value;
            for (var i = 1; i < items.Count; i++)
            {
                T item = items[i];
                int value = item.Get<ComponentStackable>().Value;
                if (value > minValue) continue;
                
                minValue = value;
                itemWithMinValue = item;
            }
            RemoveStackable(itemWithMinValue);
            
            return itemWithMinValue;
        }
    }
}