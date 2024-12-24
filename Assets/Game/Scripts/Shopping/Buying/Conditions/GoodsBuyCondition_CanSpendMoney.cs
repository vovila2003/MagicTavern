using Tavern.Storages.CurrencyStorages;

namespace Tavern.Buying
{
    public sealed class GoodsBuyCondition_CanSpendMoney : IGoodsBuyCondition 
    {
        private readonly IMoneyStorage _moneyStorage;

        public GoodsBuyCondition_CanSpendMoney(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        public bool CanBuy(Modules.Shopping.Goods goods)
        {
            int price = goods.GoodsPrice;
            return _moneyStorage.Money >= price;
        }
    }
}