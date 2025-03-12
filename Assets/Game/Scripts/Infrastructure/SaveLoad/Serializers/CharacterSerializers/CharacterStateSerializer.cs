using System.Collections.Generic;
using Tavern.Character;
using Tavern.Effects;
using Unity.Plastic.Newtonsoft.Json;

namespace Tavern.Infrastructure
{
    public class CharacterStateSerializer
    {
        private readonly ICharacter _character;
        private readonly EffectsCatalog _catalog;

        public CharacterStateSerializer(ICharacter character, EffectsCatalog catalog)
        {
            _character = character;
            _catalog = catalog;
        }

        public string Serialize()
        {
            var effects = new List<string>(_character.GetState().Effects.Count);
            foreach (EffectConfig effect in _character.GetState().Effects)
            {
                effects.Add(effect.EffectName);
            }

            return JsonConvert.SerializeObject(effects);
        }

        public void Deserialize(string value)
        {
            List<string> effectNames = JsonConvert.DeserializeObject<List<string>>(value);
            if (effectNames == null) return;

            CharacterState state = _character.GetState();
            state.RemoveAllEffects();            
            foreach (string effectName in effectNames)
            {
                if (!_catalog.TryGetEffect(effectName, out EffectConfig effect)) continue;
                
                state.AddEffect(effect);
            }
        } 
    }
}