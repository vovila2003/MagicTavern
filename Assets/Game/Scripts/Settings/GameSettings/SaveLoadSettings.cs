using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class SaveLoadSettings
    {
        [field: SerializeField]
        public string FileSaveName { get; private set; }
    }
}