using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public class CaringSettings
    {
        [SerializeField]
        private CaringType Type;
        
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
        
        public CaringType CaringType => Type;
        public float Duration => DurationInSeconds;
        public bool IsCriticalEnabled => EnableCriticalTimer;
        public float CriticalDuration => CriticalDurationInSeconds;
        public float CaringValue => Value;

        private bool MustBeGreaterThanDuration(float value) => 
            value > DurationInSeconds;
    }
}