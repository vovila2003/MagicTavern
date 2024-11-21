using Modules.Gardening;
using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(
        fileName = "SeedbedConfig", 
        menuName = "Settings/Gardening/Seedbed Config")]
    public class SeedbedSettings : ScriptableObject
    {
        [SerializeField]    
        private GameObject SeedbedPrefab;

        public GameObject Seedbed => SeedbedPrefab;
    }
}