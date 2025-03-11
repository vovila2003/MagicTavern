using JetBrains.Annotations;
using Tavern.Storages.CurrencyStorages;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class MoneySerializer : BaseStorageSerializer
    {
        public MoneySerializer(MoneyStorage storage) 
            : base(storage.Storage, nameof(MoneyStorage)) { }
    }
}