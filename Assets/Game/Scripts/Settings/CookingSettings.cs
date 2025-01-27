using System;
using Tavern.Cooking;
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

        public DishRecipeCatalog DishRecipes => DishRecipeCatalog;
        public EffectsCatalog Effects => EffectsCatalog;
    }
}