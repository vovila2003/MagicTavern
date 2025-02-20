using System.Collections.Generic;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public class CategoriesView :  View, ICategoriesView
    {
        private readonly List<FilterView> _filters = new();

        public Transform Container => transform;

        public void AddFilter(FilterView filter)
        {
            filter.transform.SetParent(transform);
            _filters.Add(filter);
        }

        public void ClearFilters()
        {
            foreach (FilterView filter in _filters)
            {
                Destroy(filter.gameObject);
            }
            
            _filters.Clear();
        }
    }
}