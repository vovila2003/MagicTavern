using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IMainMenuView : IView
    {
        event UnityAction OnStartGame;
        event UnityAction OnQuitGame;
    }
}