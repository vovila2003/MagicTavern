using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class ComponentEffectOne : IEffectComponent
    {
        [SerializeField] 
        private int EffectValue;

        [SerializeField] 
        private EffectOneConfig EffectConfig;
        
        public int Value => EffectValue;
        
        public IEffectConfig Config => EffectConfig;

        public ComponentEffectOne()
        {
        }

        public ComponentEffectOne(EffectOneConfig config, int value)
        {
            EffectConfig = config;
            EffectValue = value;
        }

        public IItemComponent Clone()
        {
            return new ComponentEffectOne(EffectConfig, EffectValue);
        }
    }
}