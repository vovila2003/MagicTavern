using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class ComponentEffect : IEffectComponent
    {
        [SerializeField] 
        private int EffectValue;

        [SerializeField] 
        private EffectConfig EffectConfig;
        
        public int Value => EffectValue;
        
        public IEffectConfig Config => EffectConfig;

        public ComponentEffect()
        {
        }

        public ComponentEffect(EffectConfig config, int value)
        {
            EffectConfig = config;
            EffectValue = value;
        }

        public IItemComponent Clone()
        {
            return new ComponentEffect(EffectConfig, EffectValue);
        }
    }
}