using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.SaveLoad;
using Tavern.Cooking;
using Tavern.Settings;
using Unity.Plastic.Newtonsoft.Json;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class DishAutoCookbookSerializer : IGameSerializer
    {
        private readonly string _name;
        private readonly DishAutoCookbookContext _cookbook;
        private readonly DishRecipeCatalog _catalog;

        public DishAutoCookbookSerializer(
            DishAutoCookbookContext cookbook,
            GameSettings settings)
        {
            _name = nameof(DishAutoCookbookContext);
            _cookbook = cookbook;
            _catalog = settings.CookingSettings.DishRecipes;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var recipes = new List<string>(_cookbook.Recipes.Count);
            foreach (ItemRecipe recipe in _cookbook.Recipes.Values)
            {
                recipes.Add(recipe.Name);
            }

            saveState[_name] = JsonConvert.SerializeObject(recipes);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(_name, out string valueString)) return;

            var recipes = JsonConvert.DeserializeObject<List<string>>(valueString);
            if (recipes == null) return;

            _cookbook.Clear();

            foreach (string name in recipes)
            {
                if (!_catalog.TryGetRecipe(name, out DishRecipe recipe)) continue;
                
                _cookbook.AddRecipe(recipe);
            }
        }
    }
}