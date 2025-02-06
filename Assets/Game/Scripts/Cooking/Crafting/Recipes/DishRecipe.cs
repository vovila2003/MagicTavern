using System.Collections.Generic;
using Modules.Crafting;
using Sirenix.OdinInspector;
using Tavern.Cooking.MiniGame;
using Tavern.ProductsAndIngredients;
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
        private PlantProductItemConfig[] PlantProductIngredients;
        
        [SerializeField] 
        private AnimalProductItemConfig[] AnimalProductIngredients;
        
        [SerializeField] 
        private KitchenItemConfig[] RequiredKitchenItems;

        [SerializeField] 
        private MiniGameConfig MiniGameConfig;

        [ShowInInspector, ReadOnly] 
        private int _stars;

        public PlantProductItemConfig[] PlantProducts => PlantProductIngredients;
        public AnimalProductItemConfig[] AnimalProducts => AnimalProductIngredients;
        public KitchenItemConfig[] KitchenItems => RequiredKitchenItems;
        public int StarsCount => _stars;
        public MiniGameConfig GameConfig => MiniGameConfig;

        private void OnValidate()
        {
            var items = new HashSet<string>();
            foreach (PlantProductItemConfig itemConfig in PlantProductIngredients)
            {
                CheckDuplicates(items, itemConfig.GetItem().ItemName);
            }
            
            foreach (AnimalProductItemConfig itemConfig in AnimalProductIngredients)
            {
                CheckDuplicates(items, itemConfig.GetItem().ItemName);
            }
            
            _stars = items.Count;
            
            foreach (KitchenItemConfig itemConfig in RequiredKitchenItems)
            {
                CheckDuplicates(items, itemConfig.GetItem().ItemName);
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