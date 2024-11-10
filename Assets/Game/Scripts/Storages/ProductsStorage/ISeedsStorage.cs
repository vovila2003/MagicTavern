using Modules.Gardening;

namespace Tavern.Storages
{
    public interface ISeedsStorage
    {
        bool TryGetStorage(PlantType type, out PlantStorage storage);
    }
}