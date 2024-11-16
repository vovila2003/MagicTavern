using System;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class LootIngredient
    {
        [SerializeField] 
        private LootItemConfig LootConfig;

        [SerializeField] 
        private int Amount;
        
        public LootItemConfig Loot => LootConfig;
        public int LootAmount => Amount;
    }
}