using System;
using Sirenix.OdinInspector;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class CookingUISettings
    {
        [SerializeField]
        private CookingAndMatchRecipeView CookingAndMatchRecipePrefab;

        [SerializeField]
        private MatchRecipeView MatchNewRecipePrefab;

        [SerializeField]
        private ContainerView CookingIngredientsPrefab;

        [SerializeField]
        private CookingMiniGameView CookingMiniGamePrefab;

        [SerializeField]
        private RecipeIngredientsView RecipeIngredientsPrefab;
        
        [SerializeField] 
        private RecipeEffectsView RecipeEffectsPrefab;
        
        [SerializeField, PreviewField]
        private Sprite DefaultIngredientSprite;

        [SerializeField] 
        private Color EmptyIngredientColor;
        
        [SerializeField] 
        private Color FilledIngredientColor;
        
        public CookingMiniGameView CookingMiniGameView => CookingMiniGamePrefab;
        public ContainerView CookingIngredientsView => CookingIngredientsPrefab;
        public MatchRecipeView MatchNewRecipeView => MatchNewRecipePrefab;
        public Sprite DefaultSprite => DefaultIngredientSprite;
        public Color EmptyColor => EmptyIngredientColor;
        public Color FilledColor => FilledIngredientColor;
        public CookingAndMatchRecipeView CookingAndMatchRecipeView => CookingAndMatchRecipePrefab;
        public RecipeIngredientsView RecipeIngredientsView => RecipeIngredientsPrefab;
        public RecipeEffectsView RecipeEffectsView => RecipeEffectsPrefab;
    }
}