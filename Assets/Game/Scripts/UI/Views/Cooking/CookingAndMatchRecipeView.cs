using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class CookingAndMatchRecipeView : View
    {
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }
    }
}