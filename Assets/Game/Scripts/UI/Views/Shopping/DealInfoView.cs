using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class DealInfoView : View, IDealInfoView
    {
        public event UnityAction OnAction
        {
            add => ActionButton.onClick.AddListener(value);
            remove => ActionButton.onClick.RemoveListener(value);
        } 
        
        public event UnityAction OnClose
        {
            add => CloseButton.onClick.AddListener(value);
            remove => CloseButton.onClick.RemoveListener(value);
        } 
        
        public event UnityAction OnPlus1
        {
            add => Plus1Button.onClick.AddListener(value);
            remove => Plus1Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnPlus3
        {
            add => Plus3Button.onClick.AddListener(value);
            remove => Plus3Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnPlus5
        {
            add => Plus5Button.onClick.AddListener(value);
            remove => Plus5Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnPlus10
        {
            add => Plus10Button.onClick.AddListener(value);
            remove => Plus10Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnMax
        {
            add => MaxButton.onClick.AddListener(value);
            remove => MaxButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnMinus1
        {
            add => Minus1Button.onClick.AddListener(value);
            remove => Minus1Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnMinus3
        {
            add => Minus3Button.onClick.AddListener(value);
            remove => Minus3Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnMinus5
        {
            add => Minus5Button.onClick.AddListener(value);
            remove => Minus5Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnMinus10
        {
            add => Minus10Button.onClick.AddListener(value);
            remove => Minus10Button.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnMin
        {
            add => MinButton.onClick.AddListener(value);
            remove => MinButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction<float> OnSliderChanged
        {
            add => Slider.onValueChanged.AddListener(value);
            remove => Slider.onValueChanged.RemoveListener(value);
        }
        
        [SerializeField]
        private TMP_Text Title;
        
        [SerializeField]
        private TMP_Text Description;
        
        [SerializeField]
        private Image Icon;

        [SerializeField] 
        private Image Extra;

        [SerializeField] 
        private Button ActionButton;

        [SerializeField] 
        private Button CloseButton;

        [SerializeField] 
        private Button Plus1Button;
        
        [SerializeField] 
        private Button Plus3Button;
        
        [SerializeField] 
        private Button Plus5Button;
        
        [SerializeField] 
        private Button Plus10Button;
        
        [SerializeField] 
        private Button MaxButton;
        
        [SerializeField] 
        private Button Minus1Button;
        
        [SerializeField] 
        private Button Minus3Button;
        
        [SerializeField] 
        private Button Minus5Button;
        
        [SerializeField] 
        private Button Minus10Button;
        
        [SerializeField] 
        private Button MinButton;

        [SerializeField] 
        private Slider Slider;

        [SerializeField] 
        private TMP_Text Amount;

        [SerializeField] 
        private EffectView[] EffectViews;

        public IEffectView[] Effects { get; private set; }
        
        private void Awake()
        {
            Effects = new IEffectView[EffectViews.Length];
            for (var i = 0; i < EffectViews.Length; i++)
            {
                Effects[i] = EffectViews[i];
            }
        }

        public void SetTitle(string title)
        {
            Title.text = title;
        }
        
        public void SetDescription(string title)
        {
            Description.text = title;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }
        
        public void HideAllEffects()
        {
            foreach (EffectView view in EffectViews)
            {
                view.SetActive(false);
            }
        }

        public void SetExtra(bool isExtra)
        {
            Extra.gameObject.SetActive(isExtra);
        }

        public void SetAmount(string amount)
        {
            Amount.text = amount;
        }

        public void SetSliderMAxValue(float value)
        {
            Slider.maxValue = value;
        }

        public void SetSliderValue(float value)
        {
            Slider.value = value;
        }
    }
}