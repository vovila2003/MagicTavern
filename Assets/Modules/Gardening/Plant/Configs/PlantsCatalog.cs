using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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

        private readonly Dictionary<string, PlantConfig> _dict = new();
        
        public PlantConfig[] PlantConfigs => Plants;

        public bool TryGetPlantConfig(string plantName, out PlantConfig config)
        {
            if (plantName is not null) return _dict.TryGetValue(plantName, out config);
            
            config = null;
            return false;
        }

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void Awake()
        {
            _dict.Clear();
            foreach (PlantConfig settings in Plants)
            {
                if (settings?.Name != null)
                {
                    _dict.Add(settings.Name, settings);
                }
            }
        }
       
        private void OnValidate()
        {
            var collection = new Dictionary<Plant, bool>();
            _dict.Clear();
            foreach (PlantConfig config in Plants)
            {
                string plantName = config.Name;
                if (plantName is null)
                {
                    Debug.LogWarning("Plant has empty name in plant catalog");
                    continue;
                }
                _dict.Add(plantName, config);

                if (collection.TryAdd(config.Plant, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate plant {config.Plant.PlantName} in catalog");
            }            
        }
    }
}