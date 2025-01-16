using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface ICookingView : IView
    {
        Transform Container { get; }
        event UnityAction OnCloseClicked;
    }
}