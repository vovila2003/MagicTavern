using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class CategoriesView :  View, ICategoriesView
    {
        public event UnityAction OnBuyOut
        {
            add => ByuOut.onClick.AddListener(value);
            remove => ByuOut.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private Transform Content;

        [SerializeField] 
        private ScrollRect ScrollRect;

        [SerializeField] 
        private Button ByuOut;

        public Transform Container => Content;

        public void Left()
        {
            ScrollRect.horizontalNormalizedPosition = 0;  
        }
    }
}