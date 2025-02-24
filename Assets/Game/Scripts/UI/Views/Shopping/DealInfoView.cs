using System;
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
        
        public event UnityAction OnMax
        {
            add => MaxButton.onClick.AddListener(value);
            remove => MaxButton.onClick.RemoveListener(value);
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

        public event Action<int> OnPlus;
        public event Action<int> OnMinus;
        
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
        private TMP_Text Price;

        [SerializeField] 
        private TMP_Text TotalPrice;

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

        private void OnEnable()
        {
            Plus1Button.onClick.AddListener(OnPlus1Clicked);
            Plus3Button.onClick.AddListener(OnPlus3Clicked);
            Plus5Button.onClick.AddListener(OnPlus5Clicked);
            Plus10Button.onClick.AddListener(OnPlus10Clicked);
            
            Minus1Button.onClick.AddListener(OnMinus1Clicked);
            Minus3Button.onClick.AddListener(OnMinus3Clicked);
            Minus5Button.onClick.AddListener(OnMinus5Clicked);
            Minus10Button.onClick.AddListener(OnMinus10Clicked);
        }

        private void OnDisable()
        {
            Plus1Button.onClick.RemoveListener(OnPlus1Clicked);
            Plus3Button.onClick.RemoveListener(OnPlus3Clicked);
            Plus5Button.onClick.RemoveListener(OnPlus5Clicked);
            Plus10Button.onClick.RemoveListener(OnPlus10Clicked);
            
            Minus1Button.onClick.RemoveListener(OnMinus1Clicked);
            Minus3Button.onClick.RemoveListener(OnMinus3Clicked);
            Minus5Button.onClick.RemoveListener(OnMinus5Clicked);
            Minus10Button.onClick.RemoveListener(OnMinus10Clicked);
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

        public void SetSliderMaxValue(float value)
        {
            Slider.maxValue = value;
        }

        public void SetSliderValue(float value)
        {
            Slider.value = value;
        }

        public void SetPrice(string text)
        {
            Price.text = text;
        }
        
        public void SetTotalPrice(string text)
        {
            TotalPrice.text = text;
        }

        private void OnPlus1Clicked() => OnPlus?.Invoke(1);
        private void OnPlus3Clicked() => OnPlus?.Invoke(3);
        private void OnPlus5Clicked() => OnPlus?.Invoke(5);
        private void OnPlus10Clicked() => OnPlus?.Invoke(10);
        private void OnMinus1Clicked() => OnMinus?.Invoke(1);
        private void OnMinus3Clicked() => OnMinus?.Invoke(3);
        private void OnMinus5Clicked() => OnMinus?.Invoke(5);
        private void OnMinus10Clicked() => OnMinus?.Invoke(10);
    }
}