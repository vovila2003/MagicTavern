using Modules.Gardening;

namespace Tavern.Storages
{
    public interface ISeedsStorage
    {
        bool TryGetStorage(Plant type, out PlantStorage storage);
    }
}