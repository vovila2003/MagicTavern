using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "PlantConfig", 
        menuName = "Settings/Gardening/Plant Config")]
    public class PlantConfig : ScriptableObject 
    {
        [SerializeField]
        public Plant Plant;

        [SerializeField] 
        private Metadata Metadata;

        private readonly Dictionary<Caring, CaringConfig> _caringSettingsMap = new();
        
        public string Name => Plant.PlantName;
        public Metadata PlantMetadata => Metadata;
        
        public bool TryGetCaring(Caring caring, out CaringConfig config) => 
            _caringSettingsMap.TryGetValue(caring, out config);

        private void OnValidate()       
        {
            var collection = new Dictionary<Caring, bool>();
            foreach (CaringConfig settings in Plant.PlantCaring)
            {
                Caring caring = settings.Caring;
                _caringSettingsMap[caring] = settings;
                if (collection.TryAdd(caring, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate caring {caring.CaringName} in PlantConfig of {Plant.PlantName}");
            }            
        }
    }
}