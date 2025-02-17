using System;
using System.Collections.Generic;
using Modules.Items;

namespace Modules.Inventories
{
    public interface IInventoryBase
    {
        event Action<Item, IInventoryBase> OnItemAdded;
        event Action<Item, IInventoryBase> OnItemRemoved;   
        void AddItem(Item item);
        void RemoveItem(Item item);
        Item RemoveItem(string name);
        void RemoveItems(string name, int count);
        int GetItemCount(string name);
        bool IsItemExists(string name);
        IReadOnlyList<Item> GetItems();
        bool FindItem(string name, out Item result);
    }
}