using Modules.Inventories;
using Modules.Items;

namespace Tavern.Utils
{
    public static class ItemExtensions
    {
        public static int GetCount(this Item item) => 
            !item.TryGet(out ComponentStackable componentStackable) ? 1 : componentStackable.Value;
    }
}