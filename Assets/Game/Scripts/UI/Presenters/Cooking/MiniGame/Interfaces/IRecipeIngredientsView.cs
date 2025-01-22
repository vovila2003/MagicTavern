using System.Collections.Generic;

namespace Tavern.UI.Presenters
{
    public interface IRecipeIngredientsView : IView
    {
        IReadOnlyList<IRecipeIngredientView> RecipeIngredients { get; }
    }
}