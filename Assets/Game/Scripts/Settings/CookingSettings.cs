using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class CookingSettings
    {
        [Space, Title("Dish Catalogs")]
        [SerializeField]
        private DishRecipeCatalog SaladCatalog;
        
        [SerializeField]
        private DishRecipeCatalog StoveCatalog;
        
        [SerializeField]
        private DishRecipeCatalog OvenCatalog;
        
        [SerializeField]
        private DishRecipeCatalog GrillCatalog;
        
        [SerializeField]
        private DishRecipeCatalog MixerCatalog;
        
        [SerializeField]
        private DishRecipeCatalog TubCatalog;
        
        [Space]
        [SerializeField]
        private EffectsCatalog EffectsCatalog;
        
        [SerializeField]
        private MiniGameConfig DefaultMiniGameSettings;

        [SerializeField] 
        private int MinDefaultMiniGameTimeInSeconds = 5;
        
        [SerializeField] 
        private int MaxDefaultMiniGameTimeInSeconds = 10;

        public Dictionary<RecipeType, DishRecipeCatalog> DishRecipes { get; private set; }

        public EffectsCatalog Effects => EffectsCatalog;
        public MiniGameConfig DefaultMiniGameConfig  => DefaultMiniGameSettings;
        public int MinDefaultTime => MinDefaultMiniGameTimeInSeconds;
        public int MaxDefaultTime => MaxDefaultMiniGameTimeInSeconds;

        public void Validate()
        {
            DishRecipes = new Dictionary<RecipeType, DishRecipeCatalog>
            {
                { RecipeType.Salad , SaladCatalog},
                { RecipeType.Stove , StoveCatalog},
                { RecipeType.Oven  , OvenCatalog},
                { RecipeType.Grill , GrillCatalog},
                { RecipeType.Mixer , MixerCatalog},
                { RecipeType.Tub   , TubCatalog},
            };    
        }
    }
}