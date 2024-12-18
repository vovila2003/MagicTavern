using System;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class GardeningSettings
    {
        [SerializeField]
        private PlantsCatalog PlantsCatalog;

        [SerializeField]
        private SeedMakerSettings SeedMakerConfig;

        [SerializeField]
        private PotSettings PotConfig;
        
        public PlantsCatalog Catalog => PlantsCatalog;
        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        public PotSettings PotSettings => PotConfig;        
    }
}