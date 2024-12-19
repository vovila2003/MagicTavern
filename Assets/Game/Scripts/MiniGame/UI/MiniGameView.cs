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
        
        public event Action OnRestartGame;
        
        [SerializeField] 
        private Slider Slider;
        
        [SerializeField]
        private RectTransform Target;

        [SerializeField] 
        private Button StartButton;
        
        [SerializeField] 
        private Button RestartButton;
        
        [SerializeField]
        private Button CloseButton;
        
        [SerializeField]
        private TMP_Text ResultText;
        
        [SerializeField]
        private TMP_Text ScoreText;

        private void OnEnable()
        {
            StartButton.onClick.AddListener(OnStartButtonClicked);
            CloseButton.onClick.AddListener(OnCloseButtonClicked);
            RestartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClicked);
            CloseButton.onClick.RemoveListener(OnCloseButtonClicked);
            RestartButton.onClick.RemoveListener(OnRestartButtonClicked);
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
        
        public void ShowRestartButton() => RestartButton.gameObject.SetActive(true);

        public void HideRestartButton() => RestartButton.gameObject.SetActive(false);

        public void SetResultText(string text) => ResultText.text = text;

        public void SetScoreText(string text) => ScoreText.text = text;

        private void OnStartButtonClicked() => OnStartGame?.Invoke();

        private void OnCloseButtonClicked() => OnCloseGame?.Invoke();

        private void OnRestartButtonClicked() => OnRestartGame?.Invoke();
    }
}