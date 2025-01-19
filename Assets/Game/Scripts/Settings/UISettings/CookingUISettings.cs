using System;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class CookingUISettings
    {
        [SerializeField]
        private CookingMiniGameView CookingMiniGamePrefab;

        [SerializeField]
        private ContainerView CookingIngredientsPrefab;
        
        [SerializeField]
        private MatchRecipeView MatchRecipePrefab;
        
        public CookingMiniGameView CookingMiniGameView => CookingMiniGamePrefab;
        public ContainerView CookingIngredientsView => CookingIngredientsPrefab;
        public MatchRecipeView MatchRecipeView => MatchRecipePrefab;
    }
}