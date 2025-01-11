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
    }
}