using Modules.Items;

namespace Tavern.Shopping
{
    public class ItemInfo
    {
        public ItemConfig Item;
        public int Price;
        public int Count;

        public ItemInfo(ItemConfig item, int price, int count)
        {
            Item = item;
            Price = price;
            Count = count;
        }
    }
}