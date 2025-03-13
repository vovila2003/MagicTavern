using System;
using Tavern.Common;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class SaveLoadSettings
    {
        [field: SerializeField]
        public string FileSaveName { get; private set; }
        
        [field: SerializeField]
        public CommonItemsCatalog CommonItemsCatalog { get; private set; }
    }
}