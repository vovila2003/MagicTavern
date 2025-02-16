using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Shopping.Buying
{
    public static class PriceCalculator
    {
        public static (bool, int) GetPrice(SellerConfig sellerConfig, ItemConfig itemConfig, int reputation)
        {
            if (!itemConfig.TryGet(out ComponentSellable componentSellable))
            {
                Debug.LogError($"item {itemConfig.Name} doesn't have sellable component");
                return (false, 0);
            }

            float currentPrice = CalculatePriceByPreferences(sellerConfig, itemConfig, componentSellable);

            currentPrice = CalculateDiscount(sellerConfig, reputation, currentPrice);

            return (true, Mathf.RoundToInt(currentPrice));
        }

        private static float CalculatePriceByPreferences(SellerConfig sellerConfig, ItemConfig itemConfig,
            ComponentSellable componentSellable)
        {
            float price = componentSellable.BasePrice;
            if (!itemConfig.TryGet(out IComponentGroup componentGroup)) return price;
            
            var factor = 1f;
            foreach (Preference preference in sellerConfig.Preferences)
            {
                if (componentGroup.GetType() == preference.Group.GetType())
                {
                    factor *= preference.Factor;
                }
            }

            return factor * price;
        }

        private static float CalculateDiscount(SellerConfig sellerConfig, int reputation, float currentPrice)
        {
            return currentPrice * (1 - sellerConfig.Discounts[reputation] / 100f);
        }
    }
}