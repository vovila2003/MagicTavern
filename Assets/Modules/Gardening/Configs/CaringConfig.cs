using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public class CaringConfig
    {
        [SerializeField] 
        private Caring PlantCaring;
        
        [SerializeField] 
        private float DurationInSeconds;

        [SerializeField] 
        private bool EnableCriticalTimer;
        
        [SerializeField, 
         ShowIf("EnableCriticalTimer"), 
         InfoBox("Must be greater than DurationInSeconds"),
         ValidateInput("MustBeGreaterThanDuration")] 
        private float CriticalDurationInSeconds;

        [SerializeField] 
        private float Value;
        
        public float Duration => DurationInSeconds;
        public bool IsCriticalEnabled => EnableCriticalTimer;
        public float CriticalDuration => CriticalDurationInSeconds;
        public float CaringValue => Value;
        public string Name => Caring.CaringName;
        public Caring Caring => PlantCaring;

        private bool MustBeGreaterThanDuration(float value) => 
            value > DurationInSeconds;
    }
}