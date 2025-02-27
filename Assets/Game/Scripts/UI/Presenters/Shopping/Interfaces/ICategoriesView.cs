using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface ICategoriesView : IView
    {
        event UnityAction OnBuyOut;
        Transform Container { get; }
        void Left();
    }
}