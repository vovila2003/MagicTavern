using System;
using System.Collections.Generic;
using Modules.Info;
using Sirenix.OdinInspector;
using Tavern.Effects;
using Tavern.Settings;

namespace Tavern.Character
{
    [Serializable]
    public class CharacterState
    {
        public event Action OnEffectsChanged;
        
        private readonly CharacterSettings _settings;
        private readonly Dictionary<string, EffectConfig> _effects = new();
        
        [ShowInInspector, ReadOnly]
        public Metadata Metadata => _settings?.Metadata;
        
        [ShowInInspector, ReadOnly]
        public Dictionary<string, EffectConfig>.ValueCollection Effects => _effects?.Values;
        
        public CharacterState(CharacterSettings settings)
        {
            _settings = settings;
        }

        [Button]
        public void AddEffect(EffectConfig effect)
        {
            if (!_effects.TryAdd(effect.EffectName, effect)) return;

            OnEffectsChanged?.Invoke();
        }

        [Button]
        public void RemoveEffectByName(string effectName)
        {
            if (!_effects.Remove(effectName)) return;
            
            OnEffectsChanged?.Invoke();
        }

        [Button]
        public void RemoveAllEffects()
        {
            _effects.Clear();
            OnEffectsChanged?.Invoke();
        }

        [Button]
        public void RemoveEffect(EffectConfig effect) => RemoveEffectByName(effect.EffectName);
    }
}