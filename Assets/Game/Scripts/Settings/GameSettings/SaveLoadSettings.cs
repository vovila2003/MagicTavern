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
        public string SaveFileName { get; private set; }
        
        [field: SerializeField]
        public string AutoSaveFileName { get; private set; }
        
        [field: SerializeField]
        public CommonItemsCatalog CommonItemsCatalog { get; private set; }
        
        [field: SerializeField]
        public bool UseCompression { get; private set; }
        
        [field: SerializeField]
        public bool UseEncryption { get; private set; }

        [field: SerializeField, ShowIf("UseEncryption"), 
                ValidateInput("ValidateKey", "The key length must be 24 characters (192 bits)")]
        public string Key { get; private set; }

        [field: SerializeField, ShowIf("UseEncryption"),
                ValidateInput("ValidateIv", "The initialization vector must be non-empty")]
        public string InitializationVector { get; private set; }

        private bool ValidateKey(string key)
        {
            return key.Length == 24;
        }
        
        private bool ValidateIv(string iv)
        {
            return iv.Length != 0;
        }
    }
}