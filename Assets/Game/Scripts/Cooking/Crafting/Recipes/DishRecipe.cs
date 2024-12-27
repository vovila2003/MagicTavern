using System;
using System.Collections.Generic;
using Modules.Crafting;
using Modules.Gardening;
using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.Common;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishRecipe",
        menuName = "Settings/Cooking/Dish Recipe")]
    public class DishRecipe : ItemRecipe<DishItem>
    {
        [Title("Ingredients"), Space(10)]
        [SerializeField]
        private ProductIngredient[] ProductIngredients;
        
        [SerializeField] 
        private LootIngredient[] LootIngredients;
        
        [SerializeField] 
        private KitchenItemConfig[] RequiredKitchenItems;
        
        public ProductIngredient[] Products => ProductIngredients;
        public LootIngredient[] Loots => LootIngredients;
        public KitchenItemConfig[] KitchenItems => RequiredKitchenItems;

        private IReadOnlyDictionary<Item,int> GetRecipeItems()
        {
            Dictionary<Item, int> recipeItems = new();
            
            foreach (ProductIngredient productIngredient in ProductIngredients)
            {
                recipeItems.Add(productIngredient.Product.Item, productIngredient.ProductAmount);
            }
            
            foreach (LootIngredient lootIngredient in LootIngredients)
            {
                recipeItems.Add(lootIngredient.Loot.Item, lootIngredient.LootAmount);
            }
            
            return recipeItems;
        }

        private void OnValidate()
        {
            CheckProductDuplicates();
            CheckLootDuplicates();
            CheckKitchenItemDuplicates();
        }

        private void CheckProductDuplicates()
        {
            if (ProductIngredients is null) return;
            
            var collection = new Dictionary<Plant, bool>();
            foreach (ProductIngredient productIngredient in ProductIngredients)
            {
                if (productIngredient is null) continue;

                ProductItem productItem = productIngredient.Product.Item;
                if (!productItem.TryGetComponent(out ComponentPlant componentPlant))
                {
                    throw new Exception($"Product ingredient {productIngredient.Product.Item.ItemName} " +
                                        "does not have a component plant");
                }

                Plant type = componentPlant.Config.Plant;
                int amount = productIngredient.ProductAmount;
                if (amount <= 0)
                {
                    Debug.LogWarning($"Amount of products of type {type} has to be greater than zero.");    
                }
                
                if (collection.TryAdd(type, true)) continue;

                throw new Exception($"Duplicate product of type {type} in recipe of {Name}");
            }
        }

        private void CheckLootDuplicates()
        {
            if (LootIngredients is null) return;
            
            var collection = new Dictionary<string, bool>();
            foreach (LootIngredient lootIngredient in LootIngredients)
            {
                if (lootIngredient.Loot is null) continue;

                LootItem lootItem = lootIngredient.Loot.Item;
                string lootName = lootItem.ItemName;
                int amount = lootIngredient.LootAmount;
                if (amount <= 0)
                {
                    Debug.LogWarning($"Amount of loot of name {lootName} has to be greater than zero.");    
                }
                
                if (collection.TryAdd(lootName, true)) continue;

                throw new Exception($"Duplicate loot of name {lootName} in recipe of {Name}");
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
                                    $"in recipe of {Name}");
            }
        }
    }
}