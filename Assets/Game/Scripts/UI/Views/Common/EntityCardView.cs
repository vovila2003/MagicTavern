using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class EntityCardView : View, IEntityCardView
    {
        public event UnityAction OnCardClicked
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
        private TMP_Text Title;
        
        [SerializeField]
        private Image Icon;
        
        [SerializeField]
        private TMP_Text Time;

        [SerializeField]
        private StarsView Stars;

        [SerializeField] 
        private Button Button;

        public Transform Transform => transform;
        
        public void SetTitle(string title)
        {
            Title.text = title;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetTime(string time)
        {
            Time.text = time;
        }

        public void SetMaxStart(int count)
        {
            Stars.SetStarsMaxCount(count);
        }

        public void SetStars(float stars)
        {
            Stars.SetStars(stars);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetSelected(bool selected)
        {
            Background.color = selected ? SelectedColor : DefaultColor;
        }
    }
}