using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.Storages;
using Tavern.Storages.CurrencyStorages;

namespace Tavern.Shopping.Buying
{
    [UsedImplicitly]
    public class Buyer : IBuyer
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly InventoryFacade _inventories;

        public Buyer(IMoneyStorage moneyStorage, InventoryFacade inventories)
        {
            _moneyStorage = moneyStorage;
            _inventories = inventories;
        }

        public bool CanBuy(int price) => _moneyStorage.CanSpendMoney(price);

        public void GiveMoney(int price) => _moneyStorage.SpendMoney(price);

        public void TakeMoney(int price) => _moneyStorage.EarnMoney(price);

        public bool TakeItem(ItemConfig itemConfig)
        {
            Item item = itemConfig.Create();
            IInventoryBase inventory = _inventories.GetInventory(itemConfig.ItemTypeName);
            inventory.AddItem(item);
            return true;
        }
    }    
}