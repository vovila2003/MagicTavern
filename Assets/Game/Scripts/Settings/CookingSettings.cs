using System;
using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class CookingSettings
    {
        [SerializeField]
        private DishRecipeCatalog DishRecipeCatalog;
        
        [SerializeField]
        private EffectsCatalog EffectsCatalog;
        
        [SerializeField]
        private MiniGameConfig DefaultMiniGameSettings;

        [SerializeField] 
        private int MinDefaultMiniGameTimeInSeconds = 5;
        
        [SerializeField] 
        private int MaxDefaultMiniGameTimeInSeconds = 10;


        public EffectsCatalog Effects => EffectsCatalog;
        public MiniGameConfig DefaultMiniGameConfig  => DefaultMiniGameSettings;
        public int MinDefaultTime => MinDefaultMiniGameTimeInSeconds;
        public int MaxDefaultTime => MaxDefaultMiniGameTimeInSeconds;
        public DishRecipeCatalog DishRecipes => DishRecipeCatalog;
    }
}