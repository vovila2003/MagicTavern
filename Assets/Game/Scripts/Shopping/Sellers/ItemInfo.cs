using Modules.Items;

namespace Tavern.Shopping
{
    public class ItemInfo
    {
        public Item Item;
        public int Price;

        public ItemInfo(Item item, int price)
        {
            Item = item;
            Price = price;
        }
    }
}