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
    public class DishRecipe : ItemRecipe
    {
        [Title("Ingredients"), Space(10)]
        [SerializeField]
        private PlantProductItemConfig[] PlantProductIngredients;
        
        [SerializeField] 
        private AnimalProductItemConfig[] AnimalProductIngredients;
        
        [SerializeField] 
        private KitchenItemConfig RequiredKitchenItem;

        [SerializeField] 
        private MiniGameConfig MiniGameConfig;

        [ShowInInspector, ReadOnly] 
        private int _stars;

        public PlantProductItemConfig[] PlantProducts => PlantProductIngredients;
        public AnimalProductItemConfig[] AnimalProducts => AnimalProductIngredients;
        public KitchenItemConfig KitchenItem => RequiredKitchenItem;
        public int StarsCount => _stars;
        public MiniGameConfig GameConfig => MiniGameConfig;

        [Button]
        public void Validate()
        {
            OnValidate();
        }

        private void Awake()
        {
            _stars = PlantProductIngredients.Length + AnimalProductIngredients.Length;
        }

        private void OnValidate()
        {
            var items = new HashSet<string>();
            foreach (PlantProductItemConfig itemConfig in PlantProductIngredients)
            {
                CheckDuplicates(items, itemConfig.Name);
            }
            
            foreach (AnimalProductItemConfig itemConfig in AnimalProductIngredients)
            {
                CheckDuplicates(items, itemConfig.Name);
            }
            
            _stars = items.Count;

            if (KitchenItem is null)
            {
                throw new UnityException($"Select required kitchen Item in DishRecipe {Name}");
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