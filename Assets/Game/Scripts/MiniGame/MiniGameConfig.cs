using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.MiniGame
{
    [CreateAssetMenu(fileName = "MiniGameConfig", 
        menuName = "Settings/Cooking/MiniGames/MiniGameConfig")]
    public class MiniGameConfig : ScriptableObject
    {
        [SerializeField,
         Range(0, 1),
         ValidateInput("ValidateTargetMin", "Must be greater than 0 and less than 1")] 
        private float TargetMin;
        
        [SerializeField,
         Range(0, 1),
         ValidateInput("ValidateTargetMax", "Must be greater than TargetMin and less than 1")] 
        private float TargetMax;
        
        [SerializeField, 
         ValidateInput("ValidateSpeedMin", "Must be greater than 0")] 
        private float SpeedMin;
        
        [SerializeField, 
         ValidateInput("ValidateSpeedMax", "Must be greater than SpeedMin")] 
        private float SpeedMax;
        
        public float TargetValueMin => TargetMin;
        public float TargetValueMax => TargetMax;
        public float SpeedValueMin => SpeedMin;
        public float SpeedValueMax => SpeedMax;

        private bool ValidateTargetMin(float value) => value is > 0 and < 1;
        private bool ValidateTargetMax(float value) => value > TargetMin && value < 1;
        private bool ValidateSpeedMin(float value) => value > 0;
        private bool ValidateSpeedMax(float value) => value > SpeedMin;
    }
}