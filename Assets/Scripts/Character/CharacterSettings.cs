using System;
using UnityEngine;

namespace Character
{
    [Serializable]
    public sealed class CharacterSettings
    {
        [SerializeField] 
        private Character Prefab;

        [SerializeField] 
        private Transform World;

        public Character CharacterPrefab => Prefab;
        public Transform WorldTransform => World;
    }
}