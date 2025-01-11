using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Cooking.MiniGame
{
    [Serializable]
    public class MiniGameConfig
    {
        [SerializeField,
         Range(0, 100),
         ValidateInput("ValidateGreenZone", 
          "Must be greater than 0 and less than 100")] 
        private int GreenZone = 10;
        
        [SerializeField,
         Range(0, 100),
         ValidateInput("ValidateYellowZone", 
          "Must be greater than 0 and less than 100 - GreenZone")] 
        private int YellowZone = 10;
        
        [SerializeField, 
         ValidateInput("ValidateSpeed", 
          "Must be greater than 0")] 
        private float Speed = 5;
        
        public float Green => GreenZone / 100.0f;
        public float Yellow => YellowZone / 100.0f;
        public float SpeedValue => Speed;

        private bool ValidateGreenZone(int value) => value is > 0 and < 100;
        
        private bool ValidateYellowZone(int value) => value > 0 && value + GreenZone < 100;

        private bool ValidateSpeed(float value) => value > 0;
    }
}