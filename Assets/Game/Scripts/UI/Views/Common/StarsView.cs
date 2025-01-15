using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class StarsView : MonoBehaviour
    {
        private const float Threshold = 0.05f;

        [SerializeField]
        private Image[] Stars;

        [Button]
        public void SetStars(float stars)
        {
            Clear();
            stars = Mathf.Min(stars, Stars.Length);
            
            var fullStars = (int) stars;
            for (var i = 0; i < fullStars; i++)
            {
                Stars[i].fillAmount = 1;
            }

            float fractional = stars - fullStars;
            if (!(fractional >= Threshold)) return;
            
            Stars[fullStars].fillAmount = fractional;
        }

        private void Clear()
        {
            for (var i = 0; i < Stars.Length; i++)
            {
                Stars[i].fillAmount = 0;
            }
        }
    }
}