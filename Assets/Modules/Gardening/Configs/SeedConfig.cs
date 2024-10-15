using Modules.Products.Plants;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Gardening
{
    [CreateAssetMenu(fileName = "SeedConfig", menuName = "Settings/Seed Settings/Seed Settings", order = 0)]
    public class SeedConfig : ScriptableObject
    {
        public PlantType Type;
        public float GrowthDurationInSeconds;
        public float HarvestValue;
        public AttributeSettings[] Attributes;
    }
}