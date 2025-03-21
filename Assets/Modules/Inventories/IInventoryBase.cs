using System;
using System.Collections.Generic;
using Modules.Items;

namespace Modules.Inventories
{
    public interface IInventoryBase
    {
        event Action<Item, int> OnItemCountChanged;
        event Action<Item, IInventoryBase> OnItemAdded;
        event Action<Item, IInventoryBase> OnItemRemoved;
        void AddItem(Item item);
        void AddItems(Item item, int count);
        void RemoveItem(Item item);
        Item RemoveItem(string name);
        bool RemoveItems(string name, int count);
        bool RemoveItems(Item item, int count);
        int GetItemCount(string name);
        bool IsItemExists(string name);
        IReadOnlyList<Item> GetItems();
        bool FindItem(string name, out Item result);
    }
}