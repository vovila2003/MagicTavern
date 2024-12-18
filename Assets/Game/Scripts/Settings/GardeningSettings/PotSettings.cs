using Tavern.Gardening;
using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(
        fileName = "PotConfig", 
        menuName = "Settings/Gardening/Pot Config")]
    public class PotSettings : ScriptableObject
    {
        [SerializeField]    
        private Pot PotPrefab;

        public Pot Pot => PotPrefab;
    }
}