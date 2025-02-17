using Modules.Items;

namespace Tavern.Shopping
{
    public static class Deal
    {
        public static bool BuyFromNpc(IBuyer buyer, NpcSeller npcSeller, ItemConfig itemConfig, int price)
        {
            if (!buyer.CanBuy(price) || !npcSeller.HasItem(itemConfig)) return false;

            buyer.SpendMoney(price);

            if (!npcSeller.GiveItem(itemConfig))
            {
                buyer.EarnMoney(price);
                return false;
            }

            if (!buyer.TakeItem(itemConfig))
            {
                npcSeller.TakeItemByConfig(itemConfig);
                buyer.EarnMoney(price);
                return false;
            }
            
            npcSeller.EarnMoney(price);

            return true;
        }

        public static bool SellToNpc(NpcSeller buyer, CharacterSeller characterSeller, Item item, int price)
        {
            if (!buyer.CanBuy(price) || !characterSeller.HasItem(item)) return false;

            buyer.SpendMoney(price);

            if (!characterSeller.GiveItem(item))
            {
                buyer.EarnMoney(price);
                return false;
            }

            if (!buyer.TakeItem(item))
            {
                characterSeller.TakeItem(item);
                buyer.EarnMoney(price);
                return false;
            }
            
            characterSeller.EarnMoney(price);

            return true;
        }

        public static bool BuyOutFromNpc(IBuyer buyer, NpcSeller npcSeller, Item item, int price)
        {
            if (!buyer.CanBuy(price) || !npcSeller.HasItem(item)) return false;

            buyer.SpendMoney(price);

            if (!npcSeller.GiveItem(item))
            {
                buyer.EarnMoney(price);
                return false;
            }

            if (!buyer.TakeItem(item))
            {
                npcSeller.TakeItem(item);
                buyer.EarnMoney(price);
                return false;
            }
            
            npcSeller.EarnMoney(price);

            return true;
        }
    }
}