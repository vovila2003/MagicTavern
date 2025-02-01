using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "EffectsCatalog",
        menuName = "Settings/Cooking/Effects/Effect Catalog")]
    public class EffectsCatalog : ScriptableObject 
    {
        [SerializeField] 
        protected EffectConfig[] Effects;
        
        private readonly Dictionary<string, EffectConfig> _effectsDict = new();

        public bool TryGetEffect(string effectName, out EffectConfig effectConfig) => 
            _effectsDict.TryGetValue(effectName, out effectConfig);

        public EffectConfig GetRandomEffect() => 
            _effectsDict.ElementAt(Random.Range(0, _effectsDict.Count)).Value;
        
        public bool TryGetRandomEffectExpect(List<ComponentEffect> existed, out EffectConfig effect)
        {
            if (existed.Count == Effects.Length)
            {
                effect = null;
                return false;
            }
            
            var pool = new Dictionary<string, EffectConfig>(_effectsDict);
            foreach (ComponentEffect component in existed)
            {
                pool.Remove(component.Config.EffectName);
            }
            
            effect = pool.ElementAt(Random.Range(0, pool.Count)).Value;
                        
            return true;
        }

        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            foreach (EffectConfig settings in Effects)
            {
                string effectName = settings.EffectName;
                _effectsDict[effectName] = settings;
                if (collection.TryAdd(effectName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate effect of name {effectName} in catalog");
            }            
        }
    }
}