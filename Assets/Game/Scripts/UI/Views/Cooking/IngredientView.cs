using System;
using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class IngredientView : MonoBehaviour, IPointerClickHandler, IIngredientView
    {
        public event Action<IIngredientView> OnRightClicked;   
        public event Action<IIngredientView> OnLeftClicked;   
        
        [SerializeField]
        private TMP_Text Title;
        
        [SerializeField]
        private Image Icon;
        
        [SerializeField]
        private Image Background;

        public void SetTitle(string title)
        {
            Title.text = title;
        }

        public void SetBackgroundColor(Color color)
        {
            Background.color = color;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftClicked?.Invoke(this);
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightClicked?.Invoke(this);
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}