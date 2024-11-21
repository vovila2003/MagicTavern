using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "PlantConfig", 
        menuName = "Settings/Gardening/Plant Config")]
    public class PlantConfig : ScriptableObject 
    {
        [SerializeField]
        public Plant Plant;

        [SerializeField] 
        private PlantMetadata Metadata;

        public string Name => Plant.PlantName;
        public PlantMetadata PlantMetadata => Metadata;
    }
}