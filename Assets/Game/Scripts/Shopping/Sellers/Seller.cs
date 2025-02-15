using System;
using System.Collections.Generic;
using Modules.Items;
using Modules.Storages;
using Sirenix.OdinInspector;
using Tavern.Shopping.Buying;

namespace Tavern.Shopping
{
    [Serializable]
    public class Seller
    {
        private readonly SellerConfig _config;
        
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<string, ItemInfo> _items = new();
        
        [ShowInInspector, ReadOnly]
        private readonly ResourceStorage _moneyStorage;
        
        [ShowInInspector, ReadOnly]
        private readonly List<ItemInfo> _characterItems = new();
        private readonly PriceCalculator _priceCalculator;
        
        public IReadOnlyCollection<ItemInfo> ItemPrices => _items.Values;
        
        public Seller(SellerConfig config)
        {
            _config = config;
            _moneyStorage = new ResourceStorage(_config.StartMoney);
            _priceCalculator = new PriceCalculator(_config);
            AddItems(_config.StartItems);
        }

        public void WeeklyUpdate()
        {
            AddItems(_config.Assortment);
            _moneyStorage.Add(_config.WeeklyMoneyBonus);
        }

        public bool HasItem(ItemConfig itemConfig) => _items.ContainsKey(itemConfig.Name);

        public bool GiveItem(ItemConfig itemConfig)
        {
            if (!_items.TryGetValue(itemConfig.Name, out ItemInfo info)) return false;

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

        public void TakeItem(ItemConfig itemConfig) => AddItem(itemConfig);

        public void TakeMoney(int price) => _moneyStorage.Add(price);

        private void AddItems(ItemConfig[] collection)
        {
            foreach (ItemConfig itemConfig in collection)
            {
                AddItem(itemConfig);
            }
        }

        private void AddItem(ItemConfig itemConfig)
        {
            if (_items.TryGetValue(itemConfig.Name, out ItemInfo itemInfo))
            {
                itemInfo.Count++;
                return;
            }
                
            (bool hasPrice, int price) = _priceCalculator.GetPrice(itemConfig);

            if (!hasPrice) return;
                
            _items.Add(itemConfig.Name, new ItemInfo(itemConfig, price, 1));
        }
    }
}