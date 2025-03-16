using JetBrains.Annotations;
using Modules.Crafting;
using Tavern.Cooking;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class DishCookbookSerializer : GameSerializer<DishCookbookData>
    {
        private readonly DishCookbookContext _cookbook;
        private readonly DishRecipeCatalog _catalog;

        public DishCookbookSerializer(
            DishCookbookContext cookbook,
            GameSettings settings)
        {
            _cookbook = cookbook;
            _catalog = settings.CookingSettings.DishRecipes;
        }

        protected override DishCookbookData Serialize()
        {
            var data = new DishCookbookData(_cookbook.Recipes.Count);
            foreach (ItemRecipe recipe in _cookbook.Recipes.Values)
            {
                data.Recipes.Add(new RecipeData
                {
                    Name = recipe.Name,
                    Stars = _cookbook.GetRecipeStars(recipe as DishRecipe)
                });
            }

            return data;
        }

        protected override void Deserialize(DishCookbookData data)
        {
            _cookbook.Clear();

            foreach (RecipeData recipeData in data.Recipes)
            {
                if (!_catalog.TryGetRecipe(recipeData.Name, out DishRecipe recipe)) continue;
                
                _cookbook.AddRecipe(recipe);
                _cookbook.SetRecipeStars(recipe, recipeData.Stars);
            }
        }
    }
}