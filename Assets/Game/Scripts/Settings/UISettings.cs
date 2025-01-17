using System;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class UISettings
    {
        [SerializeField] 
        private EntityCardSettings EntityCardSettings;
        
        [SerializeField] 
        private ItemCardSettings ItemCardSettings;

        [SerializeField]
        private PanelView PanelPrefab;

        [SerializeField]
        private ContainerView LeftGridPrefab;

        [SerializeField]
        private CookingMiniGameView CookingMiniGamePrefab;

        [SerializeField]
        private ContainerView CookingIngredientsPrefab;

        public EntityCardSettings EntityCardConfig => EntityCardSettings;
        public ItemCardSettings ItemCardConfig => ItemCardSettings;
        public ContainerView ContainerView => LeftGridPrefab;
        public PanelView Panel => PanelPrefab;
        public CookingMiniGameView CookingMiniGameView => CookingMiniGamePrefab;
        public ContainerView CookingIngredientsView => CookingIngredientsPrefab;
    }
}