using System.Collections.Generic;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class RecipeIngredientsView : View, IRecipeIngredientsView
    {
        [SerializeField] 
        private RecipeIngredientView[] Ingredients;
        
        public IReadOnlyList<IRecipeIngredientView> RecipeIngredients { get; private set; } 

        private void Awake()
        {
            RecipeIngredients = new List<IRecipeIngredientView>(Ingredients);
        }
    }
}