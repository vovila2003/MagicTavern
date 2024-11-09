using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(fileName = "SeedConfig", menuName = "Settings/Gardening/Seed Settings", order = 0)]
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
        
        public PlantType Type => PlantType;
        public float GrowthDuration => GrowthDurationInSeconds;
        public int ResultValue => HarvestValue;
        public IEnumerable<CaringSettings> PlantCaring => Carings;

        private void OnValidate()
        {
            var collection = new Dictionary<CaringType, bool>();
            foreach (CaringSettings settings in Carings)
            {
                CaringType settingsCaringType = settings.CaringType;
                if (collection.TryAdd(settingsCaringType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate caring settings of type {settingsCaringType}");
            }            
        }
    }
}