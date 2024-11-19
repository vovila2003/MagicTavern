using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "CaringConfig", 
        menuName = "Settings/Gardening/Caring Config")]
    public class Caring : ScriptableObject
    {
        [SerializeField]
        private string Name;
        
        [SerializeField] 
        private Metadata Metadata;
        
        public string CaringName => Name;
        public Metadata CaringMetadata => Metadata;
    }
}