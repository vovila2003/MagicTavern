using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.MiniGame
{
    [CreateAssetMenu(fileName = "MiniGameConfig", 
        menuName = "Settings/Cooking/MiniGames/MiniGameConfig")]
    public class MiniGameConfig : ScriptableObject
    {
        [SerializeField,
         ValidateInput("ValidateMin", "Must be between 0 and 1")] 
        private float Min;
        
        [SerializeField,
         ValidateInput("ValidateMax", "Must be between Min and 1")] 
        private float Max;

        [SerializeField, 
         ValidateInput("ValidateSpeed", "Must be greater than 0")] 
        private float Speed;
        
        public float MinValue => Min;
        public float MaxValue => Max;
        public float SpeedValue => Speed;

        private bool ValidateMin(float value) => value is >= 0 and <= 1;
        private bool ValidateMax(float value) => value > Min && value <= 1;
        private bool ValidateSpeed(float value) => value > 0;
    }
}