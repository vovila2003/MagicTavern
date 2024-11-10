using Modules.Gardening;

namespace Tavern.Storages
{
    public interface IResourcesStorage
    {
        bool TryGetStorage(CaringType type, out ResourceStorage storage);
    }
}