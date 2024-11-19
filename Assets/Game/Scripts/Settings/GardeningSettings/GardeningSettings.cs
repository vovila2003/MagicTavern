using System;
using Modules.Gardening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tavern.Settings
{
    [Serializable]
    public class GardeningSettings
    {
        [FormerlySerializedAs("SeedsCatalog")] [SerializeField]
        private PlantsCatalog PlantsCatalog;

        [SerializeField]
        private SeedMakerSettings SeedMakerConfig;

        [SerializeField]
        private SeedbedSettings SeedbedConfig;
        
        public PlantsCatalog Catalog => PlantsCatalog;
        public SeedMakerSettings SeedMakerSettings => SeedMakerConfig;
        public SeedbedSettings SeedbedSettings => SeedbedConfig;        
    }
}