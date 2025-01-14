using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public abstract class View : MonoBehaviour, IView
    {
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