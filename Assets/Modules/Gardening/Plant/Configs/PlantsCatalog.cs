using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "PlantCatalog", 
        menuName = "Settings/Gardening/Plant Catalog")]
    public class PlantsCatalog : ScriptableObject
    {
        [SerializeField] 
        private PlantConfig[] Plants;
        
        private readonly Dictionary<Plant, PlantConfig> _plantsDict = new();

        public bool TryGetPlant(Plant plant, out PlantConfig plantConfig) => 
            _plantsDict.TryGetValue(plant, out plantConfig);

        private void OnValidate()
        {
            var collection = new Dictionary<Plant, bool>();
            foreach (PlantConfig settings in Plants)
            {
                Plant plant = settings.Plant;
                _plantsDict[plant] = settings;
                if (collection.TryAdd(plant, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate plant {plant.PlantName} in catalog");
            }            
        }
    }
}