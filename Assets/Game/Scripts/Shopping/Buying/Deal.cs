using Modules.Items;

namespace Tavern.Shopping
{
    public static class Deal
    {
        public static bool BuyFromNpc(IBuyer buyer, NpcSeller npcSeller, ItemConfig itemConfig, int price, int count)
        {
            if (count == 0) return true;
            
            int total = price * count;
            if (!buyer.CanBuy(total) || 
                npcSeller.GetItemCount(itemConfig) < count) return false;

            buyer.SpendMoney(total);

            if (!npcSeller.GiveItem(itemConfig, count))
            {
                buyer.EarnMoney(total);
                return false;
            }

            if (!buyer.TakeItem(itemConfig, count))
            {
                npcSeller.TakeItemByConfig(itemConfig, count);
                buyer.EarnMoney(total);
                return false;
            }
            
            npcSeller.EarnMoney(total);

            return true;
        }

        public static bool SellToNpc(NpcSeller buyer, CharacterSeller characterSeller, Item item, int price, int count)
        {
            if (count == 0) return true;
            
            int total = price * count;
            if (!buyer.CanBuy(total) || 
                characterSeller.GetItemCount(item) < count) return false;

            buyer.SpendMoney(total);

            if (!characterSeller.GiveItem(item, count))
            {
                buyer.EarnMoney(total);
                return false;
            }

            if (!buyer.TakeItem(item, count))
            {
                characterSeller.TakeItem(item, count);
                buyer.EarnMoney(total);
                return false;
            }
            
            characterSeller.EarnMoney(total);

            return true;
        }

        public static bool BuyOutFromNpc(IBuyer buyer, NpcSeller npcSeller, Item item, int price)
        {
            if (!buyer.CanBuy(price) || 
                !npcSeller.HasItem(item)) return false;

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