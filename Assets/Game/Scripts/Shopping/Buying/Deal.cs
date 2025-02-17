using Modules.Items;

namespace Tavern.Shopping.Buying
{
    public static class Deal
    {
        public static bool BuyFromNpc(IBuyer buyer, NpcSeller npcSeller, ItemConfig itemConfig, int price)
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
                npcSeller.TakeItemByConfig(itemConfig);
                buyer.TakeMoney(price);
                return false;
            }
            
            npcSeller.TakeMoney(price);

            return true;
        }

        public static bool SellToNpc(NpcSeller buyer, CharacterSeller characterSeller, Item item, int price)
        {
            if (!buyer.CanBuy(price) || !characterSeller.HasItem(item)) return false;

            buyer.GiveMoney(price);

            if (!characterSeller.GiveItem(item))
            {
                buyer.TakeMoney(price);
                return false;
            }

            if (!buyer.TakeItem(item))
            {
                characterSeller.TakeItem(item);
                buyer.TakeMoney(price);
                return false;
            }
            
            characterSeller.TakeMoney(price);

            return true;
        }
    }
}