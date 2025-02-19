using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IFilterView : IView
    {
        event UnityAction OnCardClicked;
        void SetText(string text);
    }
}