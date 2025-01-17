using System;
using Tavern.UI.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tavern.Settings
{
    [Serializable]
    public class UISettings
    {
        [SerializeField] 
        private EntityCardSettings EntityCardSettings;

        [SerializeField]
        private PanelView PanelPrefab;

        [SerializeField]
        private LeftGridView LeftGridPrefab;

        [SerializeField]
        private CookingMiniGameView CookingMiniGamePrefab;
        
        [FormerlySerializedAs("CookingIngredientsPrefab")] [SerializeField]
        private IngredientsView IngredientsPrefab;

        public EntityCardSettings EntityCardConfig => EntityCardSettings;
        public LeftGridView LeftGridView => LeftGridPrefab;
        public PanelView Panel => PanelPrefab;
        public CookingMiniGameView CookingMiniGameView => CookingMiniGamePrefab;
        public IngredientsView IngredientsView => IngredientsPrefab;
    }
}