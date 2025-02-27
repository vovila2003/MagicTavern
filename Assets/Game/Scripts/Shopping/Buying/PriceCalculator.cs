using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Shopping
{
    public static class PriceCalculator
    {
        public static (bool, int) GetPrice(
            SellerConfig sellerConfig, 
            IHavingComponentsCapable itemConfig, 
            int reputation)
        {
            if (!itemConfig.TryGet(out ComponentSellable componentSellable))
            {
                return (false, 0);
            }

            float currentPrice = CalculatePriceByPreferences(sellerConfig, itemConfig, componentSellable);

            currentPrice = CalculateDiscount(sellerConfig, reputation, currentPrice);

            return (true, Mathf.RoundToInt(currentPrice));
        }

        public static (bool hasPrice, int price) GetPriceWithSurcharge(
            SellerConfig config, 
            IHavingComponentsCapable item, 
            int currentReputation)
        {
            if (!item.TryGet(out ComponentSellable componentSellable))
            {
                return (false, 0);
            }
            
            float price = componentSellable.BasePrice * (1 + config.Surcharges[currentReputation] / 100f);
            
            return (true, Mathf.RoundToInt(price));
        }

        private static float CalculatePriceByPreferences(
            SellerConfig sellerConfig, 
            IHavingComponentsCapable itemConfig,
            ComponentSellable componentSellable)
        {
            float price = componentSellable.BasePrice;
            if (!itemConfig.TryGet(out IComponentGroup componentGroup)) return price;
            
            var factor = 1f;
            foreach (Preference preference in sellerConfig.Preferences)
            {
                if (componentGroup.Config == preference.Group) 
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