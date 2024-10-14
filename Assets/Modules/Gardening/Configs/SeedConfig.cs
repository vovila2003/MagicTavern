using Modules.Products;
using UnityEngine;

namespace Gardening
{
    [CreateAssetMenu(fileName = "SeedConfig", menuName = "Settings/Seed Settings/Seed Settings", order = 0)]
    public class SeedConfig : ScriptableObject
    {
        public PlantType Type;
        public float GrowingDurationInSeconds;
        public float HarvestValue;
        public AttributeSettings Watering;
        public AttributeSettings Fertilization;
        public AttributeSettings Disinfection;
        public AttributeSettings Healing;
        public AttributeSettings Weeding;
    }
}