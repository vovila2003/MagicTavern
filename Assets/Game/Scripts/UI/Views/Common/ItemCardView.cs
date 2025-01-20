using System;
using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Tavern.UI.Views
{
    public class ItemCardView : View, IItemCardView, IPointerClickHandler
    {
        public event Action OnLeftClicked;
        public event Action OnRightClicked;
        
        [SerializeField]
        private Image Icon;
        
        [SerializeField]
        private TMP_Text Count;

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

        public void OnPointerClick(PointerEventData eventData)
        {
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