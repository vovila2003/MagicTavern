using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Inventories;
using Modules.Items;
using Modules.Storages;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tavern.Shopping
{
    [Serializable]
    public class NpcSeller
    {
        public event Action<int> OnSellerMoneyChanged;
        public event Action<int> OnReputationChanged;
        public event Action OnItemsChanged;
        public event Action OnCharacterItemsChanged;
        
        public SellerConfig Config { get; }

        [ShowInInspector, ReadOnly]
        private readonly Dictionary<string, ItemInfoByConfig> _items = new();

        private readonly ResourceStorage _moneyStorage;

        private IInventory<Item> _charactersItems = new StackableInventory<Item>();
        private Dictionary<Item, int> _charactersItemPrices = new();

        [ShowInInspector, ReadOnly]
        public int CurrentReputation { get; private set; }

        public IReadOnlyCollection<ItemInfoByConfig> ItemPrices => _items.Values;

        [ShowInInspector, ReadOnly]
        public IReadOnlyDictionary<Item, int> CharacterItemsPrices => _charactersItemPrices;
        
        [ShowInInspector, ReadOnly]
        public int Money => _moneyStorage?.Value ?? 0;

        public NpcSeller(SellerConfig config)
        {
            Config = config;
            _moneyStorage = new ResourceStorage(Config.StartMoney); // Unlimit
            _moneyStorage.OnResourceStorageChanged += OnMoneyChanged;
            AddItems(Config.StartItems);
        }

        private void OnMoneyChanged(int value)
        {
            OnSellerMoneyChanged?.Invoke(value);
        }

        public void Dispose()
        {
            _moneyStorage.OnResourceStorageChanged -= OnMoneyChanged;
        }

        public void WeeklyUpdate()
        {
            AddItems(Config.Assortment);
            _moneyStorage.Add(Config.WeeklyMoneyBonus);
            ExtraSellRandomCharacterItem();
        }

        public void UpdateReputation(int newReputation)
        {
            if (newReputation < 0 || newReputation > ShoppingConfig.MaxReputation)
            {
                Debug.LogError($"Reputation must be between 0 and {ShoppingConfig.MaxReputation}");
                return;
            }
            
            CurrentReputation = newReputation;
            RecalculatePrices();
            
            OnReputationChanged?.Invoke(CurrentReputation);
        }

        public (bool, int) GetItemPrice(ItemConfig itemConfig) => 
            _items.TryGetValue(itemConfig.Name, out ItemInfoByConfig info) ? (true, info.Price) : (false, 0);

        public (bool hasPrice, int price) GetItemPrice(Item item) =>
            PriceCalculator.GetPrice(Config, item, CurrentReputation);

        public bool HasItem(ItemConfig itemConfig) => _items.ContainsKey(itemConfig.Name);

        public bool HasItem(Item item, int count = 1) => _charactersItems.GetItemCount(item.ItemName)  >= count;

        public int GetItemCount(ItemConfig itemConfig) => 
            !_items.TryGetValue(itemConfig.Name, out ItemInfoByConfig info) ? 0 : info.Count;

        public bool GiveItem(ItemConfig itemConfig, int count)
        {
            if (!_items.TryGetValue(itemConfig.Name, out ItemInfoByConfig info)) return false;

            if (info.Count <= 0)
            {
                _items.Remove(itemConfig.Name);
                return false;
            }

            info.Count -= count;
            if (info.Count <= 0)
            {
                _items.Remove(itemConfig.Name);
            }
            
            OnItemsChanged?.Invoke();
            
            return true;
        }

        public void TakeItemByConfig(ItemConfig itemConfig, int count)
        {
            AddItem(itemConfig, count);
            
            OnItemsChanged?.Invoke();
        }

        public bool TakeItem(Item item, int count = 1)
        {
            if (!_charactersItems.IsItemExists(item))
            {
                (bool hasPrice, int price) = PriceCalculator.GetPriceWithSurcharge(Config, item, CurrentReputation);
                if (hasPrice)
                {
                    _charactersItemPrices.Add(item, price);
                }
            }

            _charactersItems.AddItems(item, count);

            OnCharacterItemsChanged?.Invoke();

            return true;
        }

        public void EarnMoney(int price) => _moneyStorage.Add(price);
        
        public bool CanBuy(int price) => _moneyStorage.CanSpend(price);

        public void SpendMoney(int price) => _moneyStorage.Spend(price);

        public bool GiveItem(Item item, int count)
        {
            if (!_charactersItems.RemoveItems(item, count)) return false;

            if (!_charactersItems.IsItemExists(item))
            {
                _charactersItemPrices.Remove(item);
            }
            
            OnCharacterItemsChanged?.Invoke();
            return true;
        }

        private void AddItems(ItemConfig[] collection)
        {
            foreach (ItemConfig itemConfig in collection)
            {
                AddItem(itemConfig);
            }
        }

        private void AddItem(ItemConfig itemConfig, int count = 1)
        {
            if (_items.TryGetValue(itemConfig.Name, out ItemInfoByConfig itemInfo))
            {
                itemInfo.Count += count;
                return;
            }
                
            (bool hasPrice, int price) = PriceCalculator.GetPrice(Config, itemConfig, CurrentReputation);

            if (!hasPrice) return;
                
            _items.Add(itemConfig.Name, new ItemInfoByConfig(itemConfig, price));
        }

        private void RecalculatePrices()
        {
            foreach (ItemInfoByConfig info in _items.Values)
            {
                (bool hasPrice, int price) = PriceCalculator.GetPrice(Config, info.Item, CurrentReputation);
                if (!hasPrice) continue;
                
                info.Price = price;
            }
        }

        private void ExtraSellRandomCharacterItem()
        {
            float random = Random.Range(0, 101);
            if (random > Config.SellCharacterItemByExtraPriceProbability) return;

            int count = _charactersItems.Items.Count;
            if (count == 0) return;
            Item item = _charactersItems.Items[Random.Range(0, count)];

            (bool hasPrice, int price) = PriceCalculator.GetPriceWithSurcharge(Config, item, CurrentReputation);

            if (!hasPrice) return;

            int newPrice = Mathf.RoundToInt(price * Config.ExtraSellPricePercents / 100f);
            _charactersItems.RemoveItem(item);
            if (!_charactersItems.IsItemExists(item))
            {
                _charactersItemPrices.Remove(item);
            }
            
            _moneyStorage.Add(newPrice);
            
            OnCharacterItemsChanged?.Invoke();
        }
    }
}