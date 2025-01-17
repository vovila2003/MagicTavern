using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IContainerView : IView
    {
        Transform ContentTransform { get; }
    }
}