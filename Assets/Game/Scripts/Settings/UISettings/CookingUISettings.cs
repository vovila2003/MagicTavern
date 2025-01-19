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
        private CookingMiniGameView CookingMiniGamePrefab;

        [SerializeField]
        private ContainerView CookingIngredientsPrefab;
        
        [SerializeField]
        private MatchRecipeView MatchRecipePrefab;
        
        [SerializeField]
        private RecipeEffectSettings RecipeEffectSettings;
        
        [SerializeField, PreviewField]
        private Sprite DefaultIngredientSprite;

        [SerializeField] 
        private Color EmptyIngredientColor;
        
        [SerializeField] 
        private Color FilledIngredientColor;
        
        public CookingMiniGameView CookingMiniGameView => CookingMiniGamePrefab;
        public ContainerView CookingIngredientsView => CookingIngredientsPrefab;
        public MatchRecipeView MatchRecipeView => MatchRecipePrefab;
        public Sprite DefaultSprite => DefaultIngredientSprite;
        public Color EmptyColor => EmptyIngredientColor;
        public Color FilledColor => FilledIngredientColor;
        public RecipeEffectSettings RecipeEffectConfig => RecipeEffectSettings;
    }
}