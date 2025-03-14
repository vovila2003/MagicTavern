using System.Collections.Generic;
using Modules.Items;
using Tavern.Common;
using Tavern.Shopping;

namespace Tavern.Infrastructure
{
    public class NpcSellerSerializer
    {
        private readonly ItemSerializer _itemSerializer;
        private readonly IItemsCatalog _commonCatalog;

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

        private List<ItemData> SerializeCharacterItems(NpcSeller npcSeller)
        {
            var items = new List<ItemData>(npcSeller.CharacterItems.Count);
            foreach (Item item in npcSeller.CharacterItems)
            {
                items.Add(_itemSerializer.Serialize(item));
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
            shop.NpcSeller.ClearItems();
            foreach (ShopsSerializer.ItemConfigData itemData in items)
            {
                if (!_commonCatalog.TryGetItem(itemData.Name, out ItemConfig config)) continue;
                shop.NpcSeller.AddItem(config, itemData.Count, itemData.Price);
            }
        }

        private void DeserializeCharacterItems(Shop shop, List<ItemData> items)
        {
            NpcSeller seller = shop.NpcSeller;
            seller.ClearCharacterItems();
            foreach (ItemData itemData in items)
            {
                Item item = _itemSerializer.Deserialize<Item>(itemData, _commonCatalog);
                if (item == null) continue;
                
                seller.TakeItem(item);
            }
        }
    }
}