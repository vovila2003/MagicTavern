using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class ItemCardView : View, IItemCardView
    {
        public event UnityAction OnCardClicked
        {
            add => Button.onClick.AddListener(value);
            remove => Button.onClick.RemoveListener(value);
        }
        
        [SerializeField]
        private Image Icon;
        
        [SerializeField] 
        private Button Button;

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}