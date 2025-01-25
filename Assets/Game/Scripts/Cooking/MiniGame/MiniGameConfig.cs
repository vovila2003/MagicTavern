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
         ValidateInput("ValidateMinSpeed", 
          "Must be greater than 0")] 
        private float MinSpeed = 1;
        
        [SerializeField, 
         ValidateInput("ValidateMaxSpeed", 
             "Must be greater or equal than MinSpeed")] 
        private float MaxSpeed = 2;
        
        public float Green => GreenZone / 100.0f;
        public float Yellow => YellowZone / 100.0f;
        public float MinSpeedValue => MinSpeed;
        public float MaxSpeedValue => MaxSpeed;

        private bool ValidateGreenZone(int value) => value is > 0 and < 100;
        
        private bool ValidateYellowZone(int value) => value > 0 && value + GreenZone < 100;

        private bool ValidateMinSpeed(float value) => value > 0;
        private bool ValidateMaxSpeed(float value) => value >= MinSpeed;
    }
}