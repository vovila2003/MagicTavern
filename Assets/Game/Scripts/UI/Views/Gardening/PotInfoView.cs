using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class PotInfoView : View, IPotInfoView
    {
        public event UnityAction OnWateringClicked
        {
            add => Watering.onClick.AddListener(value);
            remove => Watering.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnGatherClicked
        {
            add => Gather.onClick.AddListener(value);
            remove => Gather.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private TMP_Text Title;

        [SerializeField] 
        private Image Icon;

        [SerializeField] 
        private Slider Progress;

        [SerializeField]
        private GameObject IsFertilized;
        
        [SerializeField]
        private GameObject IsSick;
        
        [SerializeField]
        private GameObject IsWaterNeed;

        [SerializeField] 
        private Button Watering;

        [SerializeField] 
        private Button Gather;

        public void SetTitle(string title)
        {
            Title.text = title;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetProgress(float progress)
        {
            Progress.value = progress;
        }

        public void SetIsFertilized(bool isFertilized)
        {
            IsFertilized.SetActive(isFertilized);
        }

        public void SetIsSick(bool isSicked)
        {
            IsSick.SetActive(isSicked);
        }

        public void SetIsWaterNeed(bool isWaterNeed)
        {
            IsWaterNeed.SetActive(isWaterNeed);
        }

        public void SetWateringActive(bool active)
        {
            Watering.gameObject.SetActive(active);
        }
        
        public void SetGatherActive(bool active)
        {
            Gather.gameObject.SetActive(active);
        }
    }
}