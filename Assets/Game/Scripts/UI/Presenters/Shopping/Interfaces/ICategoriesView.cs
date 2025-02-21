using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ICategoriesView : IView
    {
        void AddFilter(FilterView filter);
        void ClearFilters();
        Transform Container { get; }
        void Left();
    }
}