using Modules.Crafting;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "NewDishRecipe",
        menuName = "Settings/Cooking/New DishRecipe")]
    public class DishRecipe : ItemRecipe<DishItem>
    {
        [SerializeField]
        private ProductIngredient[] ProductIngredients;
        
        [SerializeField] 
        private LootIngredient[] LootIngredients;
        
        [SerializeField] 
        private KitchenItemConfig[] NeededKitchenItems;
        
        public ProductIngredient[] Products => ProductIngredients;
        public LootIngredient[] Loots => LootIngredients;
        public KitchenItemConfig[] KitchenItems => NeededKitchenItems;
    }
}