using System;
using System.Collections.Generic;
using Modules.Items;
using Modules.Storages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Shopping
{
    [Serializable]
    public class NpcSeller
    {
        private readonly SellerConfig _config;
        
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<string, ItemInfoByConfig> _items = new();
        
        [ShowInInspector, ReadOnly]
        private readonly ResourceStorage _moneyStorage;
        
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<Item, int> _characterItems = new();

        [ShowInInspector, ReadOnly]
        public int CurrentReputation { get; private set; }
        
        public IReadOnlyCollection<ItemInfoByConfig> ItemPrices => _items.Values;
        public IReadOnlyDictionary<Item, int> CharacterItemPrices => _characterItems;
        
        public NpcSeller(SellerConfig config)
        {
            _config = config;
            _moneyStorage = new ResourceStorage(_config.StartMoney);
            AddItems(_config.StartItems);
        }

        public void WeeklyUpdate()
        {
            AddItems(_config.Assortment);
            _moneyStorage.Add(_config.WeeklyMoneyBonus);
        }

        public void UpdateReputation(int newReputation)
        {
            if (newReputation < 0 || newReputation >= ShoppingConfig.MaxReputation)
            {
                Debug.LogError($"Reputation must be between 0 and {ShoppingConfig.MaxReputation - 1}");
                return;
            }
            
            CurrentReputation = newReputation;
            RecalculatePrices();
        }

        public (bool, int) GetItemPrice(ItemConfig itemConfig) => 
            _items.TryGetValue(itemConfig.Name, out ItemInfoByConfig info) ? (true, info.Price) : (false, 0);
        
        public (bool hasPrice, int price) GetItemPrice(Item item) => 
            _characterItems.TryGetValue(item, out int price) ? (true, price) : (false, 0);

        public bool HasItem(ItemConfig itemConfig) => _items.ContainsKey(itemConfig.Name);
        public bool HasItem(Item item) => _characterItems.ContainsKey(item);

        public bool GiveItem(ItemConfig itemConfig)
        {
            if (!_items.TryGetValue(itemConfig.Name, out ItemInfoByConfig info)) return false;

            if (info.Count <= 0)
            {
                _items.Remove(itemConfig.Name);
                return false;
            }

            info.Count--;
            if (info.Count <= 0)
            {
                _items.Remove(itemConfig.Name);
            }
            
            return true;
        }

        public void TakeItemByConfig(ItemConfig itemConfig) => AddItem(itemConfig);

        public bool TakeItem(Item item)
        {
            (bool hasPrice, int price) = PriceCalculator.GetPriceWithSurcharge(_config, item, CurrentReputation);

            if (!hasPrice) return false;
                
            _characterItems.Add(item, price);

            return true;
        }

        public void EarnMoney(int price) => _moneyStorage.Add(price);

        private void AddItems(ItemConfig[] collection)
        {
            foreach (ItemConfig itemConfig in collection)
            {
                AddItem(itemConfig);
            }
        }

        private void AddItem(ItemConfig itemConfig)
        {
            if (_items.TryGetValue(itemConfig.Name, out ItemInfoByConfig itemInfo))
            {
                itemInfo.Count++;
                return;
            }
                
            (bool hasPrice, int price) = PriceCalculator.GetPrice(_config, itemConfig, CurrentReputation);

            if (!hasPrice) return;
                
            _items.Add(itemConfig.Name, new ItemInfoByConfig(itemConfig, price));
        }

        private void RecalculatePrices()
        {
            foreach (ItemInfoByConfig info in _items.Values)
            {
                (bool hasPrice, int price) = PriceCalculator.GetPrice(_config, info.Item, CurrentReputation);
                if (!hasPrice) continue;
                
                info.Price = price;
            }
        }

        public bool CanBuy(int price) => _moneyStorage.CanSpend(price);

        public void SpendMoney(int price) => _moneyStorage.Spend(price);

        public bool GiveItem(Item item) => _characterItems.Remove(item);
    }
}