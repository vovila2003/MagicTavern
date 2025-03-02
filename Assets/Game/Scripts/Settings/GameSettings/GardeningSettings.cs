using System;
using Tavern.Gardening;
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

        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        public Pot Pot => PotPrefab;
    }
}