using System.Collections.Generic;
using Tavern.Character;
using Tavern.Effects;

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

        public List<CharacterSerializer.StateData> Serialize()
        {
            var stateData = new List<CharacterSerializer.StateData>(_character.GetState().Effects.Count);
            foreach (EffectConfig effect in _character.GetState().Effects)
            {
                var data = new CharacterSerializer.StateData
                {
                    EffectName = effect.EffectName
                };
                stateData.Add(data);
            }

            return stateData;
        }

        public void Deserialize(List<CharacterSerializer.StateData> data)
        {
            CharacterState characterState = _character.GetState();
            characterState.RemoveAllEffects();            
            foreach (CharacterSerializer.StateData stateData in data)
            {
                if (!_catalog.TryGetEffect(stateData.EffectName, out EffectConfig effect)) continue;
                
                characterState.AddEffect(effect);
            }
        } 
    }
}