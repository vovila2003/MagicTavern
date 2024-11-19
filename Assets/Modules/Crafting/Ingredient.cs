using System;
using Modules.Items;
using UnityEngine;

namespace Modules.Crafting
{
    [Serializable]
    public class Ingredient<T> where T : Item
    {
        [SerializeField]
        private ItemConfig<T> ItemConfig;
        
        [SerializeField]
        private int Amount;

        protected ItemConfig<T> Item => ItemConfig;
        protected int ItemAmount => Amount;
    }
}