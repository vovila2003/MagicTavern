namespace Tavern.Storages.CurrencyStorages
{
    public class MoneyStorage : BaseResourceStorageContext, IMoneyStorage
    {
        public void EarnMoney(int value) => Add(value);

        public void SpendMoney(int value) => Spend(value);

        public int Money => Value;
    }
}
