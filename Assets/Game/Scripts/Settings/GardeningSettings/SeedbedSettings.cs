using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(
        fileName = "SeedbedConfig", 
        menuName = "Settings/Gardening/Seedbed Settings")]
    public class SeedbedSettings : ScriptableObject
    {
        [SerializeField]    
        private GameObject SeedbedPrefab;
        
        public GameObject Seedbed => SeedbedPrefab;
    }
}