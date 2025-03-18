using Modules.Items;

namespace Tavern.Shopping
{
    public class ItemInfoByConfig
    {
        public readonly ItemConfig Item;
        public int Price;
        public int Count;

        public ItemInfoByConfig(ItemConfig item, int price, int count = 1)
        {
            Item = item;
            Price = price;
            Count = count;
        }
    }
}