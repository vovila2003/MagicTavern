using Modules.Gardening;

namespace Tavern.Storages
{
    public interface IResourcesStorage
    {
        bool TryGetStorage(Caring type, out ResourceStorage storage);
    }
}