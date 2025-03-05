using System;
using Tavern.Gardening;
using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class GardeningSettings
    {
        [SerializeField]
        private SeedMakerSettings SeedMakerConfig;

        [SerializeField]    
        private Pot PotPrefab;
        
        [field: SerializeField]
        public PlantProductCatalog PlantProductCatalog { get; private set; }

        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        public Pot Pot => PotPrefab;
    }
}