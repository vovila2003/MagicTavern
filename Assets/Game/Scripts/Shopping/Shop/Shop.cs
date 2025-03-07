using System;
using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.Infrastructure;
using UnityEngine;

namespace Tavern.Shopping
{
    [Serializable]
    public class Shop : INewWeekListener
    {
        public event Action OnUpdated;
        public event Action OnNpcSellerItemsChanged;
        public event Action OnNpcCharacterItemsChanged; 
        
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
            NpcSeller.OnItemsChanged += OnNpcSellerItemsCollectionChanged;
            NpcSeller.OnCharacterItemsChanged += OnNpcSellerCharacterItemsCollectionChanged;
        }

        public void Dispose()
        {
            NpcSeller.Dispose();
            NpcSeller.OnItemsChanged -= OnNpcSellerItemsCollectionChanged;
            NpcSeller.OnCharacterItemsChanged -= OnNpcSellerCharacterItemsCollectionChanged;
        }

        public void WeeklyUpdate()
        {
            if (NpcSeller is null) return;
            
            NpcSeller.WeeklyUpdate();
            OnUpdated?.Invoke();
        }

        public void BuyByConfig(ItemConfig itemConfig, int count = 1)
        {
            if (NpcSeller.GetItemCount(itemConfig) < count)
            {
                Debug.Log($"Shop doesn't have need count of item {itemConfig.Name}");
                return;
            }
            
            (bool hasPrice, int price) = NpcSeller.GetItemPrice(itemConfig);

            if (!hasPrice)
            {
                Debug.Log($"Can't buy item {itemConfig.Name}. Unknown price.");
                return;
            }

            bool result = Deal.BuyFromNpc(_characterBuyer, NpcSeller, itemConfig, price, count);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void BuyOut(Item item, int count = 1)
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
            
            bool result = Deal.BuyOutFromNpc(_characterBuyer, NpcSeller, item, price, count);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void Sell(Item item, int count = 1)
        {
            if (_characterSeller.GetItemCount(item) < count)
            {
                Debug.Log($"Character doesn't have need count of item {item.ItemName}");
                return;
            }
            
            (bool hasPrice, int price) = _characterSeller.GetItemPrice(item);
            
            if (!hasPrice)
            {
                Debug.Log($"Can't sell item {item.ItemName}. Unknown price.");
                return;
            }
            
            bool result = Deal.SellToNpc(NpcSeller, _characterSeller, item, price, count);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void SetReputation(int reputation) => NpcSeller.UpdateReputation(reputation);
        
        void INewWeekListener.OnNewWeek(int _) => WeeklyUpdate();

        private void OnNpcSellerItemsCollectionChanged() => OnNpcSellerItemsChanged?.Invoke();

        private void OnNpcSellerCharacterItemsCollectionChanged() => OnNpcCharacterItemsChanged?.Invoke();
    }
}