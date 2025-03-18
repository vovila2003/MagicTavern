using System;
using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Tavern.UI.Views
{
    public class ItemCardView : 
        View, 
        IItemCardView, 
        IPointerClickHandler
    {
        public event Action OnLeftClicked;
        public event Action OnRightClicked;
        
        [SerializeField]
        private Image Icon;
        
        [SerializeField]
        private TMP_Text Count;

        [SerializeField] 
        private TMP_Text PriceText;

        [SerializeField]
        private GameObject Price;

        private bool _isActive;
        
        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetCount(string count)
        {
            Count.text = count;
        }

        public void SetPriceActive(bool active)
        {
            Price.SetActive(active);
        }

        public void SetPrice(string text)
        {
            PriceText.text = text;
        }

        public void SetActive(bool active)
        {
            _isActive = active;
            Icon.color = active ? Color.white : Color.gray;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isActive) return;
            
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftClicked?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightClicked?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}