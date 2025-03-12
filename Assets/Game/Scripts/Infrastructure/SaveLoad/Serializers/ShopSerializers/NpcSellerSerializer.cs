using System.Collections.Generic;
using Modules.Items;
using Tavern.Shopping;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    public class NpcSellerSerializer
    {
        private const string Money = "Money";
        private const string Reputation = "Reputation";
        private const string Items = "Items";
        private const string ItemName = "ItemName";
        private const string ItemCount = "ItemCount";
        private const string ItemPrice = "ItemPrice";
        private const string CharacterItems = "CharacterItems";
        public string Serialize(NpcSeller npcSeller)
        {
            var data = new Dictionary<string, string>
            {
                [Money] = npcSeller.Money.ToString(),
                [Reputation] = npcSeller.CurrentReputation.ToString(),
                [Items] = SerializeItems(npcSeller),
                [CharacterItems] = SerializeCharacterItems(npcSeller)
            };

            return Serializer.SerializeObject(data);
        }

        private string SerializeItems(NpcSeller npcSeller)
        {
            var items = new List<string>(npcSeller.ItemPrices.Count);
            foreach (ItemInfoByConfig itemInfo in npcSeller.ItemPrices)
            {
                var itemData = new Dictionary<string, string>
                {
                    [ItemName] = itemInfo.Item.Name,
                    [ItemCount] = itemInfo.Count.ToString(),
                    [ItemPrice] = itemInfo.Price.ToString()
                };
                items.Add(Serializer.SerializeObject(itemData));
            }
            
            return Serializer.SerializeObject(items);
        }

        private string SerializeCharacterItems(NpcSeller npcSeller)
        {
            var items = new List<string>(npcSeller.CharacterItemsPrices.Count);
            foreach ((Item item, int price) in npcSeller.CharacterItemsPrices)
            {
                var itemData = new Dictionary<string, string>
                {
                    [ItemName] = item.ItemName,
                    [ItemPrice] = price.ToString()
                };
                items.Add(Serializer.SerializeObject(itemData));
            }
            
            return Serializer.SerializeObject(items);
        }
    }
}