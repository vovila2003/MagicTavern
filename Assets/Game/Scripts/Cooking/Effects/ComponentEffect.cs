using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class ComponentEffect : IEffectComponent
    {
        [SerializeField] 
        private EffectConfig EffectConfig;
        
        public IEffectConfig Config => EffectConfig;

        public ComponentEffect()
        {
        }

        public ComponentEffect(EffectConfig config)
        {
            EffectConfig = config;
        }

        public IItemComponent Clone()
        {
            return new ComponentEffect(EffectConfig);
        }
    }
}