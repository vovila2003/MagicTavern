using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.Storages;
using Tavern.Storages.CurrencyStorages;

namespace Tavern.Shopping
{
    [UsedImplicitly]
    public class CharacterBuyer : IBuyer
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly InventoryFacade _inventories;

        public CharacterBuyer(IMoneyStorage moneyStorage, InventoryFacade inventories)
        {
            _moneyStorage = moneyStorage;
            _inventories = inventories;
        }

        public bool CanBuy(int price) => _moneyStorage.CanSpendMoney(price);

        public void SpendMoney(int price) => _moneyStorage.SpendMoney(price);

        public void EarnMoney(int price) => _moneyStorage.EarnMoney(price);

        public bool TakeItem(ItemConfig itemConfig, int count)
        {
            if (itemConfig.Has<ComponentStackable>())
            {
                Item item = itemConfig.Create();
                item.Get<ComponentStackable>().Value = count;
                return TakeItem(item);
            }

            var result = true;
            for (var i = 0; i < count; ++i)
            {
                Item item = itemConfig.Create();
                result &= TakeItem(item);
            }

            return result;
        }

        public bool TakeItem(Item item)
        {
            IInventoryBase inventory = _inventories.GetInventory(item.TypeName);
            inventory.AddItem(item);
            
            return true;
        }
    }    
}