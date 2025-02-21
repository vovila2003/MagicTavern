using System;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Shopping
{
    [Serializable]
    public class Shop : IDisposable
    {
        public event Action OnUpdated;
        
        [ShowInInspector, ReadOnly]
        public NpcSeller NpcSeller { get; private set; }

        [ShowInInspector, ReadOnly]
        private CharacterSeller _characterSeller;

        private CharacterBuyer _characterBuyer;

        public SellerConfig SellerConfig { get; private set; }

        public Shop(CharacterSeller characterSeller, CharacterBuyer characterBuyer, SellerConfig sellerConfig)
        {
            _characterSeller = characterSeller;
            _characterBuyer = characterBuyer;
            SellerConfig = sellerConfig;
            
            NpcSeller = SellerConfig.Create();
        }

        public void Dispose()
        {
            NpcSeller.Dispose();
        }

        public void WeeklyUpdate()
        {
            if (NpcSeller is null) return;
            
            NpcSeller.WeeklyUpdate();
            OnUpdated?.Invoke();
        }

        public void BuyByConfig(ItemConfig itemConfig)
        {
            if (!NpcSeller.HasItem(itemConfig))
            {
                Debug.Log($"Shop doesn't have item {itemConfig.Name}");
                return;
            }
            
            (bool hasPrice, int price) = NpcSeller.GetItemPrice(itemConfig);

            if (!hasPrice)
            {
                Debug.Log($"Can't buy item {itemConfig.Name}. Unknown price.");
                return;
            }

            bool result = Deal.BuyFromNpc(_characterBuyer, NpcSeller, itemConfig, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void BuyOut(Item item)
        {
            if (!NpcSeller.HasItem(item))
            {
                Debug.Log($"Shop doesn't have item {item.ItemName}");
                return;
            }
            
            (bool hasPrice, int price) = NpcSeller.GetItemPrice(item);
            
            if (!hasPrice)
            {
                Debug.Log($"Can't buy item {item.ItemName}. Unknown price.");
                return;
            }
            
            bool result = Deal.BuyOutFromNpc(_characterBuyer, NpcSeller, item, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void Sell(Item item)
        {
            if (!_characterSeller.HasItem(item))
            {
                Debug.Log($"Character doesn't have item {item.ItemName}");
                return;
            }
            
            (bool hasPrice, int price) = _characterSeller.GetItemPrice(item);
            
            if (!hasPrice)
            {
                Debug.Log($"Can't sell item {item.ItemName}. Unknown price.");
                return;
            }
            
            bool result = Deal.SellToNpc(NpcSeller, _characterSeller, item, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void SetReputation(int reputation)
        {
            NpcSeller.UpdateReputation(reputation);
        }
    }
}