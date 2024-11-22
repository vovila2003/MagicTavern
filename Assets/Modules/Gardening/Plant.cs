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

        [Space]
        [SerializeField,Range(0, 100)] 
        private int BaseProbabilityOfSickness;
        
        [SerializeField, Range(0, 100)] 
        private int PenaltyProbabilityOfSickness;

        [Space]
        [SerializeField]
        private int HarvestValue;

        public string PlantName => Name;
        public float GrowthDuration => GrowthDurationInSeconds;
        public int ResultValue => HarvestValue;
        public int WateringAmount => WateringCount;
        public int SicknessProbability => BaseProbabilityOfSickness;
        public int SicknessPenalty => PenaltyProbabilityOfSickness;
    }
}