using System;
using System.Collections.Generic;
using Modules.Items;

namespace Modules.Inventories
{
    public interface IInventory<T> where T : Item
    {
        event Action<T> OnItemAdded;
        event Action<T> OnItemRemoved;
        List<T> Items { get; }
        void Setup(params T[] items);
        void AddItem(T item);
        void RemoveItem(T item);
        T RemoveItem(string name);
        void RemoveItems(string name, int count);
        IReadOnlyList<T> GetItems();
        bool FindItem(string name, out T result);
        int GetItemCount(string name);
        bool FindAllItems(string name, out List<T> items);
        bool IsItemExists(T item);
    }
}