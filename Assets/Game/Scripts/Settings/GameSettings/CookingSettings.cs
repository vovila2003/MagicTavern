using System;
using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class CookingSettings
    {
        [field: SerializeField]
        public DishesCatalog DishCatalog { get; private set; }
        
        [SerializeField]
        private DishRecipeCatalog DishRecipeCatalog;

        [SerializeField] 
        private KitchenItemContext KitchenItemPrefab;
        
        [SerializeField]
        private MiniGameConfig DefaultMiniGameSettings;

        [SerializeField] 
        private int MinDefaultMiniGameTimeInSeconds = 5;
        
        [SerializeField] 
        private int MaxDefaultMiniGameTimeInSeconds = 10;
        
        [field: SerializeField]
        public bool EnableRecipeMatching { get; private set; }

        [field: SerializeField]
        public KitchenItemsCatalog KitchenItemCatalog { get; private set; }
        
        public MiniGameConfig DefaultMiniGameConfig  => DefaultMiniGameSettings;
        public int MinDefaultTime => MinDefaultMiniGameTimeInSeconds;
        public int MaxDefaultTime => MaxDefaultMiniGameTimeInSeconds;
        public DishRecipeCatalog DishRecipes => DishRecipeCatalog;
        public KitchenItemContext KitchenPrefab => KitchenItemPrefab;
    }
}