using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class EffectView : MonoBehaviour, IEffectView
    {
        [SerializeField]
        private Image Icon;

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public GameObject GetGameObject() => gameObject;
    }
}