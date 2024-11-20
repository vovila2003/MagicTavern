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

        [SerializeField] 
        private Caring Water;

        [SerializeField] 
        private Caring Heal;
        
        public GameObject Seedbed => SeedbedPrefab;
        public Caring WaterCaring => Water;
        public Caring HealCaring => Heal;
    }
}