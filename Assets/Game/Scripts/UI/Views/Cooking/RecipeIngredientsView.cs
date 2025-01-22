using System.Collections.Generic;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class RecipeIngredientsView : View, IRecipeIngredientsView
    {
        [SerializeField] 
        private IngredientView[] Ingredients;
        
        [SerializeField]
        private RecipeEffectView[] Effects;

        public IReadOnlyList<IIngredientView> RecipeIngredients { get; private set; } 
        public IReadOnlyList<IRecipeEffectView> RecipeEffects { get; private set; }

        private void Awake()
        {
            RecipeIngredients = new List<IIngredientView>(Ingredients);
            RecipeEffects = new List<IRecipeEffectView>(Effects);
        }
    }
}