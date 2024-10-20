using System;
using Modules.Gardening.Enums;
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
        
        [SerializeField] 
        private float CriticalDurationInSeconds;
        
        public CaringType CaringType => Type;
        public float Duration => DurationInSeconds;
        public bool IsCriticalEnabled => EnableCriticalTimer;
        public float CriticalDuration => CriticalDurationInSeconds;
    }
}