using System.Collections.Generic;

namespace Tavern.UI.Presenters
{
    public interface IRecipeEffectsView : IView
    {
        IReadOnlyList<IEffectView> RecipeEffects { get; }
    }
}