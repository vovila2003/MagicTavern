using UnityEngine;

namespace Tavern.Effects
{
    public interface IEffectConfig
    {
        public string EffectName { get; }
        public Sprite Icon { get; }
        
    }
}