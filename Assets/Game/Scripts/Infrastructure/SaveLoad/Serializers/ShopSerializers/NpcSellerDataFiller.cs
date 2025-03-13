using System.Collections.Generic;
using Modules.Items;
using Tavern.Shopping;

namespace Tavern.Infrastructure
{
    public class NpcSellerDataFiller
    {
        public void FillData(NpcSeller npcSeller, ShopsSerializer.ShopData shopData)
        {
            shopData.Money = npcSeller.Money;
            shopData.Reputation = npcSeller.CurrentReputation;
            shopData.Items = SerializeItems(npcSeller);
            shopData.CharacterItems = SerializeCharacterItems(npcSeller);
        }

        private List<ShopsSerializer.ItemsData> SerializeItems(
            NpcSeller npcSeller)
        {
            var items = new List<ShopsSerializer.ItemsData>(npcSeller.ItemPrices.Count);
            foreach (ItemInfoByConfig itemInfo in npcSeller.ItemPrices)
            {
                var itemData = new ShopsSerializer.ItemsData
                {
                    Name = itemInfo.Item.Name,
                    Count = itemInfo.Count,
                    Price = itemInfo.Price
                };

                items.Add(itemData);
            }

            return items;
        }

        private List<ShopsSerializer.ItemsData> SerializeCharacterItems(NpcSeller npcSeller)
        {
            var items = new List<ShopsSerializer.ItemsData>(npcSeller.CharacterItemsPrices.Count);
            foreach ((Item item, int price) in npcSeller.CharacterItemsPrices)
            {
                var itemData = new ShopsSerializer.ItemsData
                {
                    Name = item.ItemName,
                    Price = price
                };

                items.Add(itemData);
            }

            return items;
        }
    }
}