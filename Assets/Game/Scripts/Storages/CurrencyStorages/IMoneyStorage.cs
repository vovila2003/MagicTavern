using System;

namespace Tavern.Storages.CurrencyStorages
{
    public interface IMoneyStorage
    {
        event Action<int> OnMoneyChanged;
        void EarnMoney(int value);
        void SpendMoney(int value);
        bool CanSpendMoney(int value);
        int Money { get; }
        void Change(int value);
    }
}