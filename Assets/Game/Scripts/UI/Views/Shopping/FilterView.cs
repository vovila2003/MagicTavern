using TMPro;
using UnityEngine;

namespace Tavern.UI.Views.Shopping
{
    public class FilterView :  MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text Text;

        public void SetText(string text)
        {
            Text.text = text;
        }
    }
}