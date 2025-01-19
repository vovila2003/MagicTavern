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

        public CommonUISettings CommonSettings => Common;
        public CookingUISettings CookingSettings => Cooking;
    }
}