using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class PanelView : View, IPanelView
    {
        public Transform Container => ContainerTransform;
        
        public event UnityAction OnCloseClicked
        {
            add => CloseButton.onClick.AddListener(value);
            remove => CloseButton.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private Button CloseButton;
        
        [SerializeField]
        private TMP_Text Title;

        [SerializeField]
        private Transform ContainerTransform;

        public void SetTitle(string title)
        {
            Title.text = title;
        }
    }
}