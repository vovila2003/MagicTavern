using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IMatchRecipeView : IView
    {
        event UnityAction OnPressed;
    }
}
