using System;
using System.Collections.Generic;
using Modules.Items;

namespace Modules.Inventories
{
    public interface IInventory<T> : IInventoryBase where T : Item
    {
        event Action<T> OnItemAdded;
        event Action<T> OnItemRemoved;
        List<T> Items { get; }
        void Setup(params T[] items);
        IReadOnlyList<T> GetItems();
        bool FindItem(string name, out T result);
        bool FindAllItems(string name, out List<T> items);
        bool IsItemExists(T item);
    }
}
