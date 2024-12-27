using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "PlantCatalog", 
        menuName = "Settings/Gardening/Plants/Plant Catalog")]
    public class PlantsCatalog : ScriptableObject
    {
        [SerializeField] 
        private PlantConfig[] Plants;
        
        private void OnValidate()
        {
            var collection = new Dictionary<Plant, bool>();
            foreach (PlantConfig settings in Plants)
            {
                Plant plant = settings.Plant;
                if (collection.TryAdd(plant, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate plant {plant.PlantName} in catalog");
            }            
        }
    }
}