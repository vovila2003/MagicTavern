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
        private Image Background;
        
        [SerializeField]
        private Color DefaultColor = Color.white;
        
        [SerializeField]
        private Color SelectedColor;
        
        [SerializeField] 
        private TMP_Text Text;

        [SerializeField] 
        private Button Button;

        public void SetText(string text)
        {
            Text.text = text;
        }

        public void SetSelected(bool selected)
        {
            Background.color = selected ? SelectedColor : DefaultColor;
        }
    }
    
    
}