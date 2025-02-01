using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class StarsView : MonoBehaviour
    {
        private const float Threshold = 0.05f;

        [SerializeField]
        private Image[] Stars;
        
        [SerializeField]
        private Image[] ShadowStars;

        [Button]
        public void SetStarsMaxCount(int count)
        {
            Clear(ShadowStars);
            int total = Mathf.Min(count, ShadowStars.Length);
            for (var i = 0; i < total; i++)
            {
                ShadowStars[i].fillAmount = 1;
            }
        }
        
        [Button]
        public void SetStars(float stars)
        {
            Clear(Stars);
            stars = Mathf.Min(stars, Stars.Length);
            
            var fullStars = (int) stars;
            for (var i = 0; i < fullStars; i++)
            {
                Stars[i].fillAmount = 1;
            }

            float fractional = stars - fullStars;
            if (fractional < Threshold) return;
            
            Stars[fullStars].fillAmount = fractional;
        }
        
        private static void Clear(Image[] stars)
        {
            foreach (Image star in stars)
            {
                star.fillAmount = 0;
            }
        }
    }
}