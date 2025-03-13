using System.Collections.Generic;
using Modules.Items;
using Tavern.Common;
using Tavern.Shopping;

namespace Tavern.Infrastructure
{
    public class NpcSellerSerializer
    {
        private readonly ItemSerializer _itemSerializer;
        private readonly CommonItemsCatalog _commonCatalog;

        public NpcSellerSerializer(ItemSerializer itemSerializer, CommonItemsCatalog commonCatalog)
        {
            _itemSerializer = itemSerializer;
            _commonCatalog = commonCatalog;
        }

        public void Serialize(NpcSeller npcSeller, ShopsSerializer.ShopData shopData)
        {
            shopData.Money = npcSeller.Money;
            shopData.Reputation = npcSeller.CurrentReputation;
            shopData.Items = SerializeItems(npcSeller);
            shopData.CharacterItems = SerializeCharacterItems(npcSeller);
        }

        private List<ShopsSerializer.ItemConfigData> SerializeItems(
            NpcSeller npcSeller)
        {
            var items = new List<ShopsSerializer.ItemConfigData>(npcSeller.ItemPrices.Count);
            foreach (ItemInfoByConfig itemInfo in npcSeller.ItemPrices)
            {
                var itemData = new ShopsSerializer.ItemConfigData
                {
                    Name = itemInfo.Item.Name,
                    Count = itemInfo.Count,
                    Price = itemInfo.Price
                };

                items.Add(itemData);
            }

            return items;
        }

        private List<ShopsSerializer.CharacterItemData> SerializeCharacterItems(NpcSeller npcSeller)
        {
            var items = new List<ShopsSerializer.CharacterItemData>(npcSeller.CharacterItemsPrices.Count);
            foreach ((Item item, int price) in npcSeller.CharacterItemsPrices)
            {
                var itemData = new ShopsSerializer.CharacterItemData
                {
                    ItemData = _itemSerializer.Serialize(item),
                    Price = price
                };

                items.Add(itemData);
            }

            return items;
        }

        public void Deserialize(Shop shop, ShopsSerializer.ShopData shopData)
        {
            shop.NpcSeller.SetMoney(shopData.Money);
            shop.SetReputation(shopData.Reputation);
            DeserializeItems(shop, shopData.Items);
            DeserializeCharacterItems(shop, shopData.CharacterItems);
        }

        private void DeserializeItems(Shop shop, List<ShopsSerializer.ItemConfigData> items)
        {
            //clear
            foreach (ShopsSerializer.ItemConfigData itemData in items)
            {
                if (!_commonCatalog.TryGetItem(itemData.Name, out ItemConfig config)) continue;
                //TODO
            }
        }

        private void DeserializeCharacterItems(Shop shop, List<ShopsSerializer.CharacterItemData> items)
        {
            //clearS
            foreach (ShopsSerializer.CharacterItemData itemData in items)
            {
                //if (!_commonCatalog.TryGetItem(itemData.Name, out ItemConfig config)) continue;
                
                //TODO
                
            }
        }
    }
}