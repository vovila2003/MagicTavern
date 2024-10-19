using System;
using System.Collections.Generic;
using Modules.Gardening.Enums;
using Modules.Products.Plants;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(fileName = "SeedConfig", menuName = "Settings/Seed Settings/Seed Settings", order = 0)]
    public class SeedConfig : ScriptableObject
    {
        public PlantType Type;
        public float GrowthDurationInSeconds;
        public float HarvestValue;
        public CaringSettings[] Carings;

        private void OnValidate()
        {
            var collection = new Dictionary<CaringType, bool>();
            foreach (CaringSettings settings in Carings)
            {
                if (collection.TryAdd(settings.Type, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate caring settings of type {settings.Type}");
            }            
        }
    }
}