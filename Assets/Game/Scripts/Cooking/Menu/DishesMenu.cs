using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishesMenu", 
        menuName = "Settings/Cooking/Dishes Menu")]
    public class DishesMenu : ScriptableObject
    {
        [SerializeField] 
        private MenuItem[] Prices;
        
        private readonly Dictionary<DishItemConfig, float> _prices = new();

        public IReadOnlyDictionary<DishItemConfig, float> Menu => _prices;
        
        public bool TryGetPrice(DishItemConfig config, out float price)
        {
            return _prices.TryGetValue(config, out price);
        }

        private void OnValidate()
        {
            var collection = new Dictionary<DishItemConfig, bool>();
            foreach (MenuItem menuItem in Prices)
            {
                DishItemConfig dishConfig = menuItem.Dish;
                if (dishConfig is null) continue;

                float price = menuItem.Price;
                if (price <= 0)
                {
                    Debug.LogWarning($"Price of dish {dishConfig.Name} must be greater than zero!");
                } 
                
                _prices[dishConfig] = price;
                if (collection.TryAdd(dishConfig, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate dish of name {dishConfig.Name} in menu");
            }
        }
    }
}