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
                DishItemConfig dish = menuItem.Dish;
                if (dish is null) continue;

                float price = menuItem.Price;
                if (price <= 0)
                {
                    Debug.LogWarning($"Price of dish {dish.GetItem().ItemName} must be greater than zero!");
                } 
                
                _prices[dish] = price;
                if (collection.TryAdd(dish, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate dish of name {dish.GetItem().ItemName} in menu");
            }
        }
    }
}