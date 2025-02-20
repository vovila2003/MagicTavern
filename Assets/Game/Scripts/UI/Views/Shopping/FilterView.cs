using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class FilterView :  View, IFilterView
    {
        public event UnityAction OnFilterClicked
        {
            add => Button.onClick.AddListener(value);
            remove => Button.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private TMP_Text Text;

        [SerializeField] 
        private Button Button;

        public void SetText(string text)
        {
            Text.text = text;
        }
    }
    
    
}