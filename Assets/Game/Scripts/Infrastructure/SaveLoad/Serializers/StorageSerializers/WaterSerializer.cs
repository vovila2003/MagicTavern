using JetBrains.Annotations;
using Tavern.Storages;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class WaterSerializer : BaseStorageSerializer
    {
        public WaterSerializer(WaterStorage storage) 
            : base(storage.Storage, nameof(WaterStorage)) { }
    }
}