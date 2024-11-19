using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedsCatalog", 
        menuName = "Settings/Gardening/Seeds Catalog")]
    public class SeedsCatalog : ScriptableObject
    {
        [SerializeField] 
        private SeedConfig[] Seeds;
        
        private readonly Dictionary<PlantType, SeedConfig> _seedsDict = new();

        public bool TryGetSeed(PlantType plantType, out SeedConfig seedConfig) => 
            _seedsDict.TryGetValue(plantType, out seedConfig);

        private void OnValidate()
        {
            var collection = new Dictionary<PlantType, bool>();
            foreach (SeedConfig settings in Seeds)
            {
                PlantType plantType = settings.Type;
                _seedsDict[plantType] = settings;
                if (collection.TryAdd(plantType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate seed of type {plantType} in catalog");
            }            
        }
    }
}