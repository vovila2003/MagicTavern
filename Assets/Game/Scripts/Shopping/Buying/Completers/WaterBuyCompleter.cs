using JetBrains.Annotations;
using Tavern.Goods;
using Tavern.Storages;

namespace Tavern.Buying
{
    [UsedImplicitly]
    public class WaterBuyCompleter : IGoodsBuyCompleter
    {
        private readonly IWaterStorage _storage;

        public WaterBuyCompleter(IWaterStorage storage)
        {
            _storage = storage;
        }

        public void CompleteBuy(Modules.Shopping.Goods goods)
        {
            if (goods.GoodsComponent is not WaterComponent) return;

            _storage.AddWater(1);
        }
    }
}