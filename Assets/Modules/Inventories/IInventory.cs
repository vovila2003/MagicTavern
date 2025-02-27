using System.Collections.Generic;
using Modules.Items;

namespace Modules.Inventories
{
    public interface IInventory<T> : IInventoryBase where T : Item
    {
        List<T> Items { get; }
        void Setup(params T[] items);
        bool FindAllItems(string name, out List<T> items);
        bool IsItemExists(T item);
    }
}
