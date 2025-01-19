using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class RecipeEffectView : MonoBehaviour, IRecipeEffectView
    {
        [SerializeField]
        private Image Icon;

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }
    }
}