using Tavern.UI.Views;

namespace Tavern.UI.Presenters
{
    public interface ICategoriesView : IView
    {
        void AddFilter(FilterView filter);
        void ClearFilters();
    }
}