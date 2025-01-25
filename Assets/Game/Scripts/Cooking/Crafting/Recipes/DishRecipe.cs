using System.Collections.Generic;
using Modules.Crafting;
using Sirenix.OdinInspector;
using Tavern.Cooking.MiniGame;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishRecipe",
        menuName = "Settings/Cooking/DishRecipes/Dish Recipe")]
    public class DishRecipe : ItemRecipe<DishItem>
    {
        [Title("Ingredients"), Space(10)]
        [SerializeField]
        private ProductItemConfig[] ProductIngredients;
        
        [SerializeField] 
        private LootItemConfig[] LootIngredients;
        
        [SerializeField] 
        private KitchenItemConfig[] RequiredKitchenItems;

        [SerializeField] 
        private int Stars;

        [SerializeField] 
        private MiniGameConfig MiniGameConfig;
        
        public ProductItemConfig[] Products => ProductIngredients;
        public LootItemConfig[] Loots => LootIngredients;
        public KitchenItemConfig[] KitchenItems => RequiredKitchenItems;
        public int StarsCount => Stars;
        public MiniGameConfig GameConfig => MiniGameConfig;

        private void OnValidate()
        {
            var items = new HashSet<string>();
            foreach (ProductItemConfig itemConfig in ProductIngredients)
            {
                CheckDuplicates(items, itemConfig.Item.ItemName);
            }
            
            foreach (LootItemConfig itemConfig in LootIngredients)
            {
                CheckDuplicates(items, itemConfig.Item.ItemName);
            }
            
            foreach (KitchenItemConfig itemConfig in RequiredKitchenItems)
            {
                CheckDuplicates(items, itemConfig.Item.ItemName);
            }
        }

        private void CheckDuplicates(HashSet<string> items, string itemName)
        {
            if (!items.Add(itemName))
            {
                throw new UnityException($"Duplicate item named {itemName} in DishRecipe {Name}");
            }
        }
    }
}