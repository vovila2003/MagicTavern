namespace Tavern.Storages.CurrencyStorages
{
    public interface IMoneyStorage
    {
        void EarnMoney(int value);
        void SpendMoney(int value);
        bool CanSpendMoney(int value);
        int Money { get; }
    }
}