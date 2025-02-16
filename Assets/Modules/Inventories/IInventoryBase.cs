using Modules.Items;

namespace Modules.Inventories
{
    public interface IInventoryBase
    {
        void AddItem(Item item);
        void RemoveItem(Item item);
        Item RemoveItem(string name);
        void RemoveItems(string name, int count);
        int GetItemCount(string name);
        bool IsItemExists(string name);
    }
}