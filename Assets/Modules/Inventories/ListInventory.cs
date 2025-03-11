using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Items;

namespace Modules.Inventories
{
    public class ListInventory<T> : IInventory<T> where T : Item
    {
        public event Action<Item, int> OnItemCountChanged;
        public event Action<Item, IInventoryBase> OnItemAdded;
        public event Action<Item, IInventoryBase> OnItemRemoved;

        private readonly Dictionary<T, int> _counts = new();

        public List<T> Items { get; private set; }

        public ListInventory(params T[] items)
        {
            Items = new List<T>(items);
            SetupCounts();
        }

        public void Setup(params T[] items)
        {
            Items = new List<T>(items);
            SetupCounts();
        }

        public void AddItem(Item item)
        {
            if (item is not T tItem) return;
            
            if (Items.Contains(tItem)) return;
            
            Items.Add(tItem);
            _counts.TryAdd(tItem, 0);
            _counts[tItem]++;
            OnItemAdded?.Invoke(tItem, this);
        }

        public void AddItems(Item item, int count)
        {
            if (count != 1) return;
            AddItem(item);
        }

        public void RemoveItem(Item item)
        {
            if (item is not T tItem) return;
            
            if (!Items.Remove(tItem)) return;
            
            OnItemRemoved?.Invoke(tItem, this);
            
            _counts[tItem]--;
            if (_counts[tItem] != 0) return;
            
            _counts.Remove(tItem);
        }

        public bool RemoveItems(string name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Item item = RemoveItem(name);
                if (item is null) return false;
            }

            return true;
        }

        public bool RemoveItems(Item item, int count)
        {
            if (count != 1) return false;
            RemoveItem(item);

            return true;
        }

        public Item RemoveItem(string name)
        {
            if (FindItem(name, out Item item))
            {
                RemoveItem(item);
            }

            return item;
        }

        public IReadOnlyList<Item> GetItems()
        {
            return Items.ToList();
        }

        public bool FindItem(string name, out Item result)
        {
            foreach (T inventoryItem in Items)
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
            return Items.Count(it => it.ItemName == name);
        }

        public bool FindAllItems(string name, out List<T> items)
        {
            items = new List<T>();
            bool result = false;
            foreach (T item in Items)
            {
                if (item.ItemName != name) continue;
                items.Add(item);
                result = true;
            }

            return result;
        }

        public bool IsItemExists(T item)
        {
            return Items.Contains(item);
        }

        public void Clear()
        {
            var items = new List<T>(Items);
            
            foreach (T item in items)
            {
                RemoveItem(item);
            }
            
            _counts.Clear();
            Items.Clear();
        }

        public bool IsItemExists(string name) => FindItem(name, out _);

        private void SetupCounts()
        {
            _counts.Clear();
            foreach (T item in Items)
            {
                _counts.TryAdd(item, 0);
                _counts[item]++;
            }
        }
    }
}