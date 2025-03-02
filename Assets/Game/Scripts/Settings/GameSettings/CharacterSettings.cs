using System;
using Modules.Info;
using UnityEngine;

namespace Tavern.Settings
{
   [Serializable]
    public sealed class CharacterSettings
    {
        [field: SerializeField]
        public Character.Character CharacterPrefab { get; private set; }

        [field: SerializeField] 
        public float Speed { get; private set; }
        
        [field: SerializeField] 
        public int MaxHealth { get; private set; }
        
        [field: SerializeField]
        public Metadata Metadata { get; private set; }
    }
}