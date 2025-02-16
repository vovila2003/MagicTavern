using Modules.Items;

namespace Tavern.Shopping.Buying
{
    public static class Deal
    {
        public static bool SellFromNpc(IBuyer buyer, NpcSeller npcSeller, ItemConfig itemConfig, int price)
        {
            if (!buyer.CanBuy(price) || !npcSeller.HasItem(itemConfig)) return false;

            buyer.GiveMoney(price);

            if (!npcSeller.GiveItem(itemConfig))
            {
                buyer.TakeMoney(price);
                return false;
            }

            if (!buyer.TakeItem(itemConfig))
            {
                npcSeller.TakeItem(itemConfig);
                buyer.TakeMoney(price);
                return false;
            }
            
            npcSeller.TakeMoney(price);

            return true;
        }
    }
}