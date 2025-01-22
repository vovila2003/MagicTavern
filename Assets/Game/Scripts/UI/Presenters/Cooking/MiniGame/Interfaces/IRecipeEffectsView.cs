using System.Collections.Generic;

namespace Tavern.UI.Presenters
{
    public interface IRecipeEffectsView : IView
    {
        IReadOnlyList<IRecipeEffectView> RecipeEffects { get; }
    }
}