using System;
using Modules.Products;
using UnityEngine;

namespace Gardening
{
    [CreateAssetMenu(fileName = "SeedConfig", menuName = "Settings/Seed Settings/Seed Settings 2", order = 0)]
    public class SeedConfig2 : ScriptableObject
    {
        public PlantType Type;
        public float GrowingDurationInSeconds;
        public float HarvestValue;
        public AttributeSettings2[] Attributes;

        private void OnValidate()
        {
            //
        }
    }
}