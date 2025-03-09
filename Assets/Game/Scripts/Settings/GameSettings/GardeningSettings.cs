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
        private Pot PotPrefab;
        
        [field: SerializeField]
        public PlantProductCatalog PlantProductCatalog { get; private set; }

        [field: SerializeField]
        public SeedCatalog SeedCatalog { get; private set; }
        
        [field: SerializeField]
        public AnimalProductCatalog AnimalProductCatalog { get; private set; }
        
        public Pot Pot => PotPrefab;
        
        
    }
}