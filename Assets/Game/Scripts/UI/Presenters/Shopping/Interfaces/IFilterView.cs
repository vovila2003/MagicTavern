using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IFilterView : IView
    {
        event UnityAction OnFilterClicked;
        void SetText(string text);
    }
}