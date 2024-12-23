using Modules.Gardening;
using Modules.Storages;

namespace Tavern.Storages
{
    public interface IProductsStorage
    {
        bool TryGetStorage(Plant type, out PlantStorage storage);
        PlantStorage CreateStorage(PlantConfig config, LimitType limitType = LimitType.Unlimited, int maxValue = 0);
    }
}