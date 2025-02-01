using System;
using Modules.Items;

namespace Modules.Inventories
{
    public interface IStackableInventory<T> : IInventory<T> where T : Item
    {
        event Action<T, int> OnItemCountChanged;
    }
}