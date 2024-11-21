using System;
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
        private int WateringCount; 

        [SerializeField]
        private int HarvestValue;

        public string PlantName => Name;
        public float GrowthDuration => GrowthDurationInSeconds;
        public int ResultValue => HarvestValue;
        public int WateringAmount => WateringCount;
    }
}