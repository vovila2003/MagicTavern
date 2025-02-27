using Modules.Items;

namespace Tavern.Shopping
{
    public class ItemInfoByConfig
    {
        public ItemConfig Item;
        public int Price;
        public int Count;

        public ItemInfoByConfig(ItemConfig item, int price)
        {
            Item = item;
            Price = price;
            Count = 1;
        }
    }
}