using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.MiniGame.UI
{
    public class MiniGameView : MonoBehaviour, IMiniGameView
    {
        public event Action OnStartGame;
        public event Action OnCloseGame;
        
        [SerializeField] 
        private Slider Slider;
        
        [SerializeField]
        private RectTransform Target;

        [SerializeField] 
        private Button StartButton;
        
        [SerializeField]
        private Button CloseButton;
        
        [SerializeField]
        private TMP_Text ResultText;

        private void OnEnable()
        {
            StartButton.onClick.AddListener(OnStartButtonClicked);
            CloseButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClicked);
            CloseButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void SetSliderValue(float value) => Slider.value = value;

        public void SetTarget(float start, float end)
        {
            Target.anchorMin = new Vector2(start, Target.anchorMin.y);
            Target.anchorMax = new Vector2(end, Target.anchorMax.y);
        }

        public void ShowStartButton() => StartButton.gameObject.SetActive(true);

        public void HideStartButton() => StartButton.gameObject.SetActive(false);
        
        public void ShowCloseButton() => CloseButton.gameObject.SetActive(true);

        public void HideCloseButton() => CloseButton.gameObject.SetActive(false);
        public void SetResultText(string text) => ResultText.text = text;

        private void OnStartButtonClicked() => OnStartGame?.Invoke();

        private void OnCloseButtonClicked() => OnCloseGame?.Invoke();
    }
}