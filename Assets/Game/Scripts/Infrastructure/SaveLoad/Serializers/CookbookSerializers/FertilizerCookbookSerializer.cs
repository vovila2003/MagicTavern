using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.SaveLoad;
using Tavern.Gardening.Fertilizer;
using Tavern.Settings;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class FertilizerCookbookSerializer : IGameSerializer
    {
        private readonly string _name;
        private readonly FertilizerCookbookContext _cookbook;
        private readonly FertilizerRecipeCatalog _catalog;

        public FertilizerCookbookSerializer(
            FertilizerCookbookContext cookbook,
            GameSettings settings)
        {
            _name = nameof(FertilizerCookbookContext);
            _cookbook = cookbook;
            _catalog = settings.GardeningSettings.FertilizerRecipeCatalog;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var recipes = new List<string>(_cookbook.Recipes.Count);
            foreach (ItemRecipe recipe in _cookbook.Recipes.Values)
            {
                recipes.Add(recipe.Name);
            }

            saveState[_name] = Serializer.SerializeObject(recipes);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(_name, out string valueString)) return;

            var recipes = Serializer.DeserializeObject<List<string>>(valueString);
            if (recipes == null) return;

            _cookbook.Clear();

            foreach (string name in recipes)
            {
                if (!_catalog.TryGetRecipe(name, out FertilizerRecipe recipe)) continue;
                
                _cookbook.AddRecipe(recipe);
            }
        }
    }
}