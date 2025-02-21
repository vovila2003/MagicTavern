using System.Collections.Generic;
using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class CategoriesView :  View, ICategoriesView
    {
        [SerializeField] 
        private Transform Content;

        [SerializeField] 
        private ScrollRect ScrollRect;
        
        private readonly List<FilterView> _filters = new();

        public Transform Container => Content;

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

        public void Left()
        {
            ScrollRect.horizontalNormalizedPosition = 0;  
        }
    }
}