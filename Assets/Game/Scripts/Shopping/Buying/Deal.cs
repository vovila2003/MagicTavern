using Modules.Items;

namespace Tavern.Shopping.Buying
{
    public static class Deal
    {
        public static bool Make(IBuyer buyer, Seller seller, ItemConfig itemConfig, int price)
        {
            if (!buyer.CanBuy(price) || !seller.HasItem(itemConfig)) return false;

            buyer.GiveMoney(price);

            if (!seller.GiveItem(itemConfig))
            {
                buyer.TakeMoney(price);
                return false;
            }

            if (!buyer.TakeItem(itemConfig))
            {
                seller.TakeItem(itemConfig);
                buyer.TakeMoney(price);
                return false;
            }
            
            seller.TakeMoney(price);

            return true;
        }
    }
}