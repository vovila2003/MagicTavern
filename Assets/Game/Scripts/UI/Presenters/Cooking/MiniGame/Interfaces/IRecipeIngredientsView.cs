using System.Collections.Generic;

namespace Tavern.UI.Presenters
{
    public interface IRecipeIngredientsView : IView
    {
        IReadOnlyList<IIngredientView> RecipeIngredients { get; }
        IReadOnlyList<IRecipeEffectView> RecipeEffects { get; }
    }
}