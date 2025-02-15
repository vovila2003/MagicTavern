using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Shopping.Buying
{
    public class PriceCalculator
    {
        private readonly SellerConfig _config;

        public PriceCalculator(SellerConfig config)
        {
            _config = config;
        }

        public (bool, int) GetPrice(ItemConfig itemConfig)
        {
            if (!itemConfig.TryGet(out ComponentSellable componentSellable))
            {
                Debug.LogError($"item {itemConfig.Name} doesn't have sellable component");
                return (false, 0);
            }
            
            //calculate price
            return (true, componentSellable.BasePrice);
        }
    }
}