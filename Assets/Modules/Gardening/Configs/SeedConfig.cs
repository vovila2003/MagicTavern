using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedConfig", 
        menuName = "Settings/Gardening/Seed Settings")]
    public class SeedConfig : ScriptableObject
    {
        [SerializeField]
        private PlantType PlantType;

        [SerializeField]
        private float GrowthDurationInSeconds;

        [SerializeField]
        private int HarvestValue;

        [SerializeField]
        private CaringSettings[] Carings;

        private readonly Dictionary<CaringType, CaringSettings> _caringSettingsMap = new();
        
        public PlantType Type => PlantType;
        public float GrowthDuration => GrowthDurationInSeconds;
        public int ResultValue => HarvestValue;
        public IEnumerable<CaringSettings> PlantCaring => Carings;

        public bool TryGetCaringSettings(CaringType caringType, out CaringSettings settings) => 
            _caringSettingsMap.TryGetValue(caringType, out settings);

        private void OnValidate()       
        {
            var collection = new Dictionary<CaringType, bool>();
            foreach (CaringSettings settings in Carings)
            {
                CaringType settingsCaringType = settings.CaringType;
                _caringSettingsMap[settingsCaringType] = settings;
                if (collection.TryAdd(settingsCaringType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate caring settings of type {settingsCaringType}");
            }            
        }
    }
}