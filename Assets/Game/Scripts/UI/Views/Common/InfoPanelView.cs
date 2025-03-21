using System;
using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class InfoPanelView : View, IInfoPanelView
    {
        public event UnityAction OnAction
        {
            add => ActionButton.onClick.AddListener(value);
            remove => ActionButton.onClick.RemoveListener(value);
        } 
        
        public event UnityAction OnClose
        {
            add
            {
                CloseButton.onClick.AddListener(value); 
                OkButton.onClick.AddListener(value);
            }
            remove
            {
                CloseButton.onClick.RemoveListener(value);
                OkButton.onClick.RemoveListener(value);
            }
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
        private TMP_Text ActionButtonText;

        [SerializeField] 
        private Button ActionButton;

        [SerializeField] 
        private Button CloseButton;

        [SerializeField] 
        private Button OkButton;

        [SerializeField] 
        private EffectView[] EffectViews;

        [SerializeField] 
        private GameObject DialogButtons;
        
        [SerializeField] 
        private GameObject InfoButtons;

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
        
        public void SetActionButtonText(string text)
        {
            ActionButtonText.text = text;
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

        public void SetMode(InfoPresenter.Mode mode)
        {
            switch (mode)
            {
                case InfoPresenter.Mode.Dialog:
                    InfoButtons.SetActive(false);
                    DialogButtons.SetActive(true);
                    break;
                case InfoPresenter.Mode.Info:
                    InfoButtons.SetActive(true);
                    DialogButtons.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}