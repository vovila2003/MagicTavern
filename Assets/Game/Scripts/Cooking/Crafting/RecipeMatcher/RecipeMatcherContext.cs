using System.Collections.Generic;
using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.Cooking.MiniGame;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;
using VContainer;

namespace Tavern.Cooking
{
    public class RecipeMatcherContext : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private List<string> _items = new();

        private RecipeMatcher _matcher;
        private MiniGamePlayer _player;

        [Inject]
        private void Construct(RecipeMatcher matcher, MiniGamePlayer player)
        {
            _matcher = matcher;
            _player = player;
        }

        [Button]
        public void ClearItems()
        {
            _items.Clear();
        }

        [Button]
        public void AddProductIngredientByConfig(ItemConfig<ProductItem> config)
        {
            if (config is null) return;
            
            _items.Add(config.Item.ItemName);
        }
        
        [Button]
        public void AddLootIngredientByConfig(ItemConfig<LootItem> config)
        {
            if (config is null) return;
            
            _items.Add(config.Item.ItemName);
        }
        
        [Button]
        public void AddIngredient(string itemName)
        {
            if (itemName is null) return;
            
            _items.Add(itemName);
        }

        [Button]
        public void TryMatch()
        {
            (bool result, DishRecipe recipe) = _matcher.MatchRecipe(_items);
            Debug.Log(result ? $"Recipe name is {recipe.Name}" : "Mismatch recipe");
            if (result)
            {
                _player.CreateGame(recipe);
            }
        }
    }
}