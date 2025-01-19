using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.Cooking.MiniGame.UI
{
    public class MiniGameView : MonoBehaviour, IMiniGameView
    {
        public event Action OnStartGame;
        
        [SerializeField] 
        private Slider Slider;
        
        [SerializeField]
        private RectTransform Target;
        
        [SerializeField]
        private RectTransform LeftYellow;
        
        [SerializeField]
        private RectTransform RightYellow;

        [SerializeField] 
        private Button StartButton;
        
        [SerializeField]
        private TMP_Text ScoreText;

        private void OnEnable()
        {
            StartButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnDisable()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void SetSliderValue(float value) => Slider.value = value;

        public void SetRegions(Regions regions)
        {
            Target.anchorMin = new Vector2(regions.YellowGreen, Target.anchorMin.y);
            Target.anchorMax = new Vector2(regions.GreenYellow, Target.anchorMax.y);
            LeftYellow.anchorMin = new Vector2(regions.RedYellow, Target.anchorMin.y);
            LeftYellow.anchorMax = new Vector2(regions.YellowGreen, Target.anchorMax.y);
            RightYellow.anchorMin = new Vector2(regions.GreenYellow, Target.anchorMin.y);
            RightYellow.anchorMax = new Vector2(regions.YellowRed, Target.anchorMax.y);
        }

        public void ShowStartButton() => StartButton.gameObject.SetActive(true);

        public void HideStartButton() => StartButton.gameObject.SetActive(false);

        public void SetScoreText(string text) => ScoreText.text = text;

        private void OnStartButtonClicked() => OnStartGame?.Invoke();
    }
}