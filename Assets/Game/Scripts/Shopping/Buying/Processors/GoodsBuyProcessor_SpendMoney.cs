// using Tavern.Storages.CurrencyStorages;
//
// namespace Tavern.Shopping.Buying
// {
//     public sealed class GoodsBuyProcessor_SpendMoney : IGoodsBuyProcessor 
//     {
//         private readonly IMoneyStorage _moneyStorage;
//
//         public GoodsBuyProcessor_SpendMoney(IMoneyStorage moneyStorage)
//         {
//             this._moneyStorage = moneyStorage;
//         }
//
//         public void ProcessBuy(Modules.Shopping.Goods goods)
//         {
//             _moneyStorage.SpendMoney(goods.GoodsPrice);
//         }
//     }
// }