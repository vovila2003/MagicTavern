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
        private AnimalProductIngredient[] AnimalProductIngredients;
        
        [SerializeField] 
        private KitchenItemConfig[] RequiredKitchenItems;

        [SerializeField] 
        private MiniGameConfig MiniGameConfig;

        [ShowInInspector, ReadOnly] 
        private int _stars;

        public PlantProductItemConfig[] PlantProducts => PlantProductIngredients;
        public AnimalProductIngredient[] AnimalProducts => AnimalProductIngredients;
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
            
            foreach (AnimalProductIngredient itemConfig in AnimalProductIngredients)
            {
                string itemName;
                if (!itemConfig.FromGroup)
                {
                    itemName = itemConfig.AnimalProductConfig.GetItem().ItemName;
                }
                else
                {
                    if (!itemConfig.AnimalProductConfig.GetItem().TryGet(out GroupComponent groupComponent))
                    {
                        throw new UnityException("Animal ingredient marked as Grouped, but haven't group component");
                    }
                    
                    itemName = groupComponent.GroupName;
                }
                
                if (!items.Add(itemName))
                {
                    throw new UnityException($"Duplicate item named {itemName} in DishRecipe {Name}");
                }
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