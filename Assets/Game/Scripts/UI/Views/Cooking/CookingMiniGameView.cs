using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class CookingMiniGameView : View, ICookingMiniGameView
    {
        public event UnityAction OnButtonClicked
        {
            add => Button.onClick.AddListener(value);
            remove => Button.onClick.RemoveListener(value);
        }

        [SerializeField] 
        private Image Icon;
        
        [SerializeField] 
        private TMP_Text TimeText;

        [SerializeField] 
        private RectTransform GreenZone;
        
        [SerializeField] 
        private RectTransform YellowZone;

        [SerializeField] 
        private Slider Slider;
        
        [SerializeField]
        private Button Button;
        
        [SerializeField]
        private TMP_Text ButtonText;

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetTimerText(string text)
        {
            TimeText.text = text;
        }

        public void SetSliderValue(float value)
        {
            Slider.value = value;
        }

        public void SetGreenZone(float min, float max)
        {
            GreenZone.anchorMin = new Vector2(min, 0);
            GreenZone.anchorMax = new Vector2(max, 1);
        }

        public void SetYellowZone(float min, float max)
        {
            YellowZone.anchorMin = new Vector2(min, 0);
            YellowZone.anchorMax = new Vector2(max, 1);
        }

        public void SetStartButtonActive(bool value)
        {
            Button.interactable = value;
        }

        public void SetButtonText(string text)
        {
            ButtonText.text = text;
        }
    }
}
