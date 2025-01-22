using System.Collections.Generic;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class RecipeEffectsView : View, IRecipeEffectsView
    {
        [SerializeField]
        private RecipeEffectView[] Effects;

        public IReadOnlyList<IRecipeEffectView> RecipeEffects { get; private set; }

        private void Awake()
        {
            RecipeEffects = new List<IRecipeEffectView>(Effects);
        }
    }
}