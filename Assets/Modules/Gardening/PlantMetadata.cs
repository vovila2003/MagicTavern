using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public class PlantMetadata : Metadata
    {
        private const int AgeCount = 3;
        
        [Space]
        [SerializeField, 
         ValidateInput("Validate", "Arrays size must be 3"),
         PreviewField]
        public Sprite[] Healthy = new Sprite[AgeCount];
        
        [SerializeField, 
         ValidateInput("Validate", "Arrays size must be 3"),
         PreviewField]
        public Sprite[] Drying = new Sprite[AgeCount];
        
        [SerializeField, 
         ValidateInput("Validate", "Arrays size must be 3"),
         PreviewField]
        public Sprite[] Sick = new Sprite[AgeCount];

        private bool Validate() => Drying.Length == AgeCount;
    }
}