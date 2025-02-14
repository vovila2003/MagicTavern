using UnityEditor;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "PlantConfig", 
        menuName = "Settings/Gardening/Plants/Plant Config")]
    public class PlantConfig : ScriptableObject 
    {
        [SerializeField]
        public Plant Plant;

        [SerializeField] 
        private PlantMetadata Metadata;

        public string Name => Plant.PlantName;
        public PlantMetadata PlantMetadata => Metadata;

        private void OnValidate()
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);
            Plant.SetName(System.IO.Path.GetFileNameWithoutExtension(path));
#endif
        }
    }
}