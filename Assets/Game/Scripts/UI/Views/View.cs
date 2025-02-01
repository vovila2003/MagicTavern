using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}