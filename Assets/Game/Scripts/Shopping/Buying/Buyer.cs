using System;
using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Modules.Storages;
using Tavern.Storages;

namespace Tavern.Shopping.Buying
{
    [UsedImplicitly]
    public class Buyer : IBuyer
    {
        private readonly ResourceStorage _moneyStorage;
        private readonly InventoryFacade _inventories;

        public Buyer(ResourceStorage moneyStorage, InventoryFacade inventories)
        {
            _moneyStorage = moneyStorage;
            _inventories = inventories;
        }

        public bool CanBuy(int price) => _moneyStorage.CanSpend(price);

        public bool GiveMoney(int price) => _moneyStorage.Spend(price);

        public void TakeMoney(int price) => _moneyStorage.Add(price);

        public bool TakeItem(ItemConfig itemConfig)
        {
            Item item = itemConfig.Create();
            Type type = item.GetType();
            IInventoryBase inventory = _inventories.GetInventory(type);
            inventory.AddItem(item);
            return true;
        }
    }    
}