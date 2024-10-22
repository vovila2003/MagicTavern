using Modules.Gardening;

namespace Tavern.Storages
{
    public interface IProductsStorage
    {
        bool TryGetStorage(PlantType type, out PlantStorage storage);
    }
}