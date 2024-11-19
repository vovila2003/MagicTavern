using Modules.Gardening;

namespace Tavern.Storages
{
    public interface IProductsStorage
    {
        bool TryGetStorage(Plant type, out PlantStorage storage);
    }
}