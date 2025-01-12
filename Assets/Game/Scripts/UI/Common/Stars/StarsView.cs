using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI
{
    public class StarsView : MonoBehaviour
    {
        private const float Threshold = 0.2f;

        [SerializeField]
        private Image[] Stars;

        private void Awake()
        {
            Clear();
        }

        [Button]
        public void SetStars(float stars)
        {
            Clear();
            stars = Mathf.Min(stars, Stars.Length);
            
            var fullStars = (int) stars;
            for (var i = 0; i < fullStars; i++)
            {
                Stars[i].gameObject.SetActive(true);
            }

            float fractional = stars - fullStars;
            if (!(fractional >= Threshold)) return;
            
            Stars[fullStars].fillAmount = fractional;
            Stars[fullStars].gameObject.SetActive(true);
        }

        private void Clear()
        {
            for (var i = 0; i < Stars.Length; i++)
            {
                Stars[i].fillAmount = 1;
                Stars[i].gameObject.SetActive(false);
            }
        }
    }
}