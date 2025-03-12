using Modules.Crafting;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [CreateAssetMenu(
        fileName = "FertilizerRecipeCatalog", 
        menuName = "Settings/Gardening/Fertilizer/Fertilizer Recipe Catalog")]
    public class FertilizerRecipeCatalog : RecipeCatalog<FertilizerRecipe>
    {
    }
}