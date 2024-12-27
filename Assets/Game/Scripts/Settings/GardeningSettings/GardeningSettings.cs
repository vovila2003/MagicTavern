using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class GardeningSettings
    {
        [SerializeField]
        private SeedMakerSettings SeedMakerConfig;

        [SerializeField]
        private PotSettings PotConfig;
        
        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        public PotSettings PotSettings => PotConfig;        
    }
}