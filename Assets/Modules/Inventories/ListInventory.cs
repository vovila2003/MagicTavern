using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Items;

namespace Modules.Inventories
{
    public class ListInventory<T> : IInventory<T> where T : Item
    {
        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;
        public event Action<T, int> OnItemCountIncreased;
        public event Action<T, int> OnItemCountDecreased;

        private List<T> _items;
        
        public List<T> Items => _items;

        public ListInventory(params T[] items)
        {
            _items = new List<T>(items);
        }

        public void Setup(params T[] items)
        {
            _items = new List<T>(items);
        }

        public void AddItem(T item)
        {
            if (_items.Contains(item)) return;
            
            _items.Add(item);
            OnItemAdded?.Invoke(item);
        }
        
        public void RemoveItem(T item)
        {
            if (_items.Remove(item))
            {
                OnItemRemoved?.Invoke(item);
            }
        }

        public void RemoveItems(string name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveItem(name);
            }
        }

        public T RemoveItem(string name)
        {
            if (FindItem(name, out T item))
            {
                RemoveItem(item);
            }

            return item;
        }

        public IReadOnlyList<T> GetItems()
        {
            return _items.ToList();
        }

        public bool FindItem(string name, out T result)
        {
            foreach (T inventoryItem in _items)
            {
                if (inventoryItem.ItemName != name) continue;
                
                result = inventoryItem;
                return true;
            }
            
            result = null;
            return false;
        }

        public int GetItemCount(string name)
        {
            return _items.Count(it => it.ItemName == name);
        }

        public bool FindAllItems(string name, out List<T> items)
        {
            items = new List<T>();
            bool result = false;
            foreach (T item in _items)
            {
                if (item.ItemName != name) continue;
                items.Add(item);
                result = true;
            }

            return result;
        }

        public bool IsItemExists(T item)
        {
            return _items.Contains(item);
        }
    }
}