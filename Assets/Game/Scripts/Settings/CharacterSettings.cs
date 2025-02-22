using Modules.Info;
using UnityEngine;

namespace Tavern.Settings
{
    [CreateAssetMenu(
        fileName = "CharacterSettings", 
        menuName = "Settings/Character Settings/Character Settings")]
    public sealed class CharacterSettings : ScriptableObject
    {
        [SerializeField]
        private Character.Character CharacterPrefab;

        [SerializeField]
        private float Speed;
        
        [SerializeField]
        private int MaxHealth;
        
        [field: SerializeField]
        public Metadata Metadata { get; private set; }
        
        public Character.Character Prefab => CharacterPrefab;
        public float InitSpeed => Speed;
        public int Health => MaxHealth;
    }
}