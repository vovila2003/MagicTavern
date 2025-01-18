using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class IngredientView : MonoBehaviour
    {
        public event UnityAction OnIngredientClicked
        {
            add => Button.onClick.AddListener(value);
            remove => Button.onClick.RemoveListener(value);
        }
        
        [SerializeField]
        private TMP_Text Title;
        
        [SerializeField]
        private Image Icon;
        
        [SerializeField]
        private Image Background;

        [SerializeField] 
        private Button Button;

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
    }
}