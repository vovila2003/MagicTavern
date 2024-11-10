using System;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class GardeningSettings
    {
        [SerializeField]
        private SeedsCatalog SeedsCatalog;

        [SerializeField]
        private SeedMakerSettings SeedMakerConfig;

        [SerializeField]
        private SeedbedSettings SeedbedConfig;
        
        public SeedsCatalog Catalog => SeedsCatalog;
        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        public SeedbedSettings SeedbedSettings => SeedbedConfig;        
    }
}