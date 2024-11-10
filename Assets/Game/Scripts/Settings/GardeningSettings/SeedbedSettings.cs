using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(fileName = "SeedbedConfig", menuName = "Settings/Gardening/Seedbed Settings", order = 0)]
    public class SeedbedSettings : ScriptableObject
    {
        [SerializeField]    
        private GameObject SeedbedPrefab;
        
        [SerializeField]
        private Transform SeedbedParent;
                
        public GameObject Seedbed => SeedbedPrefab;
        public Transform Parent => SeedbedParent;
    }
}