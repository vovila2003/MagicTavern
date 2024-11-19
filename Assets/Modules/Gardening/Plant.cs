using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public class Plant
    {
        [SerializeField] 
        private string Name;
        
        [SerializeField]
        private float GrowthDurationInSeconds;

        [SerializeField]
        private int HarvestValue;

        [SerializeField]
        private CaringConfig[] Carings;

        public string PlantName => Name;
        public float GrowthDuration => GrowthDurationInSeconds;
        public int ResultValue => HarvestValue;
        public IEnumerable<CaringConfig> PlantCaring => Carings;
    }
}