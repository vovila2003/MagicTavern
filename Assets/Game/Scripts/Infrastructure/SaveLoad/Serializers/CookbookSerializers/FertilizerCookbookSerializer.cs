using JetBrains.Annotations;
using Modules.Crafting;
using Tavern.Gardening.Fertilizer;
using Tavern.Settings;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class FertilizerCookbookSerializer : GameSerializer<FertilizerCookbookData>
    {
        private readonly FertilizerCookbookContext _cookbook;
        private readonly FertilizerRecipeCatalog _catalog;

        public FertilizerCookbookSerializer(
            FertilizerCookbookContext cookbook,
            GameSettings settings)
        {
            _cookbook = cookbook;
            _catalog = settings.GardeningSettings.FertilizerRecipeCatalog;
        }

        protected override FertilizerCookbookData Serialize()
        {
            var data = new FertilizerCookbookData(_cookbook.Recipes.Count);
            foreach (ItemRecipe recipe in _cookbook.Recipes.Values)
            {
                data.Recipes.Add(recipe.Name);
            }

            return data;
        }

        protected override void Deserialize(FertilizerCookbookData data)
        {
            _cookbook.Clear();

            foreach (string name in data.Recipes)
            {
                if (!_catalog.TryGetRecipe(name, out FertilizerRecipe recipe)) continue;
                
                _cookbook.AddRecipe(recipe);
            }
        }
    }
}