using System;

namespace Tavern.Storages.CurrencyStorages
{
    public class MoneyStorage :
        BaseResourceStorageContext, 
        IMoneyStorage
    {
        public event Action<int> OnMoneyChanged;
        public void EarnMoney(int value) => Add(value);

        public void SpendMoney(int value) => Spend(value);
        public bool CanSpendMoney(int value) => CanSpend(value);

        public int Money => Value;

        protected override void OnEnable()
        {
            base.OnEnable();
            Storage.OnResourceStorageChanged += OnChanged;
        }

        protected override void OnDisable()
        {
            Storage.OnResourceStorageChanged -= OnChanged;
        }

        private void OnChanged(int value)
        {
            OnMoneyChanged?.Invoke(value);            
        }
    }
}
