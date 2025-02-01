using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IPauseView : IView
    {
        event UnityAction OnResume;
    }
}