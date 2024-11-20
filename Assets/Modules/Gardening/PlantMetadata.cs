using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public class PlantMetadata : Metadata
    {
        [Space]
        [SerializeField, 
         PreviewField]
        public Sprite[] Healthy = new Sprite[3];
        
        [SerializeField, 
         ValidateInput("DryingValidate", "Arrays must have the same size"),
         PreviewField]
        public Sprite[] Drying = new Sprite[3];
        
        [SerializeField, 
         ValidateInput("SickValidate", "Arrays must have the same size"),
         PreviewField]
        public Sprite[] Sick = new Sprite[3];

        [SerializeField,
         PreviewField] 
        public Sprite Lost;

        private bool DryingValidate() => Drying.Length == Healthy.Length;
        private bool SickValidate() => Sick.Length == Healthy.Length;
    }
}