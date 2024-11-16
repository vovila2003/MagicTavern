using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Cooking
{
    public class DishCrafterContext : MonoBehaviour
    {
        private DishCrafter _dishCrafter;

        [Inject]
        private void Construct(DishCrafter dishCrafter)
        {
            _dishCrafter = dishCrafter;
        }

        private void OnEnable() => _dishCrafter.OnCrafted += OnCrafted;

        private void OnDisable() => _dishCrafter.OnCrafted -= OnCrafted;

        [Button]
        public void Craft(DishRecipe recipe)
        {
            if (!_dishCrafter.CanCraft(recipe))
            {
                Debug.Log($"Can't craft dish of name {recipe.ResultItem.Item.ItemName}");    
                return;
            } 
            
            _dishCrafter.Craft(recipe).Forget();
        }

        private void OnCrafted(DishItem dish)
        {
            Debug.Log($"Dish with name {dish.ItemName} is crafted!");
        }
    }
}