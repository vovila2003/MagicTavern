using System;
using System.Collections.Generic;
using Modules.Crafting;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishRecipe",
        menuName = "Settings/Cooking/Dish Recipe")]
    public class DishRecipe : ItemRecipe<DishItem>
    {
        [SerializeField]
        private ProductIngredient[] ProductIngredients;
        
        [SerializeField] 
        private LootIngredient[] LootIngredients;
        
        [SerializeField] 
        private KitchenItemConfig[] RequiredKitchenItems;
        
        public ProductIngredient[] Products => ProductIngredients;
        public LootIngredient[] Loots => LootIngredients;
        public KitchenItemConfig[] KitchenItems => RequiredKitchenItems;
        
        private void OnValidate()
        {
            CheckProductDuplicates();
            CheckLootDuplicates();
            CheckKitchenItemDuplicates();
        }

        private void CheckProductDuplicates()
        {
            if (ProductIngredients is null) return;
            
            var collection = new Dictionary<PlantType, bool>();
            foreach (var productIngredient in ProductIngredients)
            {
                if (productIngredient is null) continue;
                
                PlantType type = productIngredient.Type;
                if (collection.TryAdd(type, true)) continue;

                throw new Exception($"Duplicate product of type {type} in recipe of {ResultItem.Item.ItemName}");
            }
        }

        private void CheckLootDuplicates()
        {
            if (LootIngredients is null) return;
            
            var collection = new Dictionary<string, bool>();
            foreach (LootIngredient lootIngredient in LootIngredients)
            {
                if (lootIngredient.Loot is null) continue;
                
                string lootName = lootIngredient.Loot.Item.ItemName;
                if (collection.TryAdd(lootName, true)) continue;

                throw new Exception($"Duplicate loot of name {lootName} in recipe of {ResultItem.Item.ItemName}");
            }
        }

        private void CheckKitchenItemDuplicates()
        {
            if (KitchenItems is null) return;
            
            var collection = new Dictionary<string, bool>();
            foreach (KitchenItemConfig kitchenItemConfig in KitchenItems)
            {
                if (kitchenItemConfig is null) continue;
                
                string kitchenItemName = kitchenItemConfig.Item.ItemName;
                if (collection.TryAdd(kitchenItemName, true)) continue;

                throw new Exception($"Duplicate kitchen item of name {kitchenItemName} " +
                                    $"in recipe of {ResultItem.Item.ItemName}");
            }
        }
    }
}