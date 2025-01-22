using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ICookingAndMatchRecipeView : IView
    {
        Transform Transform { get; }
    }
}