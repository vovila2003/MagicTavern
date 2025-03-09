using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Storages.CurrencyStorages;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class MoneySerializer : IGameSerializer
    {
        private readonly IMoneyStorage _moneyStorage;

        public MoneySerializer(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            saveState["Money"] = _moneyStorage.Money.ToString();
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue("Money", out string moneyString)) return;
            
            if (int.TryParse(moneyString, out int value))
            {
                _moneyStorage.Change(value);
            }
        }
    }
}