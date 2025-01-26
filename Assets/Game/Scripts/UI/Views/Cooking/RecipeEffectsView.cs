using System.Collections.Generic;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class RecipeEffectsView : View, IRecipeEffectsView
    {
        [SerializeField]
        private EffectView[] Effects;

        public IReadOnlyList<IEffectView> RecipeEffects { get; private set; }

        private void Awake()
        {
            RecipeEffects = new List<IEffectView>(Effects);
        }
    }
}