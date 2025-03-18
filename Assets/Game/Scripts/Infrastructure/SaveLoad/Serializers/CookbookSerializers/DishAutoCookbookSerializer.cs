using JetBrains.Annotations;
using Modules.Crafting;
using Tavern.Cooking;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class DishAutoCookbookSerializer : GameSerializer<DishAutoCookbookData>
    {
        private readonly DishAutoCookbookContext _cookbook;
        private readonly DishRecipeCatalog _catalog;

        public DishAutoCookbookSerializer(
            DishAutoCookbookContext cookbook,
            GameSettings settings)
        {
            _cookbook = cookbook;
            _catalog = settings.CookingSettings.DishRecipes;
        }

        protected override DishAutoCookbookData Serialize()
        {
            var data = new DishAutoCookbookData(_cookbook.Recipes.Count);
            foreach (ItemRecipe recipe in _cookbook.Recipes.Values)
            {
                data.Recipes.Add(recipe.Name);
            }

            return data;
        }

        protected override void Deserialize(DishAutoCookbookData data)
        {
            _cookbook.Clear();

            foreach (string name in data.Recipes)
            {
                if (!_catalog.TryGetRecipe(name, out DishRecipe recipe)) continue;
                
                _cookbook.AddRecipe(recipe);
            }
        }
    }
}