using Modules.Gardening;
using Tavern.Goods;
using Tavern.Storages;

namespace Tavern.Buying
{
    public class ProductBuyCompleter : IGoodsBuyCompleter
    {
        private readonly IProductsStorage _storage;

        public ProductBuyCompleter(IProductsStorage storage)
        {
            _storage = storage;
        }

        public void CompleteBuy(Modules.Shopping.Goods goods)
        {
            if (goods.GoodsComponent is not ProductComponent component) return;

            PlantConfig plantConfig = component.Config;

            if (!_storage.TryGetStorage(plantConfig.Plant, out PlantStorage plantStorage))
            {
                plantStorage = _storage.CreateStorage(plantConfig);
            }

            plantStorage.Add(goods.GoodsComponent.Count);
        }
    }
}