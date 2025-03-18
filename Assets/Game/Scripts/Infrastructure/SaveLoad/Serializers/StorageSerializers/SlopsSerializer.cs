using JetBrains.Annotations;
using Tavern.Storages;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public sealed class SlopsSerializer : BaseStorageSerializer
    {
        public SlopsSerializer(SlopsStorage storage) 
            : base(storage.Storage, nameof(SlopsStorage)) { }
    }
}