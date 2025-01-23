using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IMatchNewRecipeView : IView
    {
        event UnityAction OnPressed;
    }
}
