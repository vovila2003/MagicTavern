using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class VendorInfoView : View, IVendorInfoView
    {
        [SerializeField] 
        private TMP_Text Title;

        [SerializeField] 
        private Image Icon;
        
        [SerializeField]
        private StarsView Stars;

        [SerializeField] 
        private TMP_Text Money;

        public void SetTitle(string text)
        {
            Title.text = text;
        }

        public void SetIcon(Sprite sprite)
        {
            Icon.sprite = sprite;
        }
        
        public void SetMaxStart(int count)
        {
            Stars.SetStarsMaxCount(count);
        }

        public void SetStars(float stars)
        {
            Stars.SetStars(stars);
        }

        public void SetMoney(string text)
        {
            Money.text = text;
        }
    }
}