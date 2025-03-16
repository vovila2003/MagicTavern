using System;
using Sirenix.OdinInspector;
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
        
        [field: SerializeField]
        public bool UseCompression { get; private set; }
        
        [field: SerializeField]
        public bool UseEncryption { get; private set; }

        [field: SerializeField, ShowIf("UseEncryption")]
        public string Key { get; private set; }

        [field: SerializeField, ShowIf("UseEncryption")]
        public string InitializationVector { get; private set; }
    }
}