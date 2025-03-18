using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class UISettings
    {
        [SerializeField] 
        private CommonUISettings Common;

        [SerializeField] 
        private CookingUISettings Cooking;
        
        [field: SerializeField] 
        public ShoppingUISettings Shopping { get; private set; }
        
        [field: SerializeField]
        public GardeningUISettings Gardening { get; private set; }

        public CommonUISettings CommonSettings => Common;
        public CookingUISettings CookingSettings => Cooking;
    }
}