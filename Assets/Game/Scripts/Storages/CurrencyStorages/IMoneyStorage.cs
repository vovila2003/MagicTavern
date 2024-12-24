namespace Tavern.Storages.CurrencyStorages
{
    public interface IMoneyStorage
    {
        void EarnMoney(int value);
        void SpendMoney(int value);
        int Money { get; }
    }
}