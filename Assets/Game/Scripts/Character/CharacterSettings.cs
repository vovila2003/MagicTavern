using UnityEngine;

namespace Tavern.Character
{
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "Settings/Character Settings/Character Settings")]
    public sealed class CharacterSettings : ScriptableObject
    {
        [SerializeField]
        private Character CharacterPrefab;

        [SerializeField]
        private float Speed;
        
        [SerializeField]
        private int MaxHealth;
        
        public Character Prefab => CharacterPrefab;
        public float InitSpeed => Speed;
        public int Health => MaxHealth;
    }
}