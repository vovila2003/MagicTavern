using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ILeftGridView : IView
    {
        Transform ContentTransform { get; }
    }
}