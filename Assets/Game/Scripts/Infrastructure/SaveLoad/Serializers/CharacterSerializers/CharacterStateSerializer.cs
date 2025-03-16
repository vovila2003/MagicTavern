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

        public List<CharacterStateData> Serialize()
        {
            var stateData = new List<CharacterStateData>(_character.GetState().Effects.Count);
            foreach (EffectConfig effect in _character.GetState().Effects)
            {
                var data = new CharacterStateData
                {
                    EffectName = effect.EffectName
                };
                stateData.Add(data);
            }

            return stateData;
        }

        public void Deserialize(List<CharacterStateData> data)
        {
            CharacterState characterState = _character.GetState();
            characterState.RemoveAllEffects();            
            foreach (CharacterStateData stateData in data)
            {
                if (!_catalog.TryGetEffect(stateData.EffectName, out EffectConfig effect)) continue;
                
                characterState.AddEffect(effect);
            }
        } 
    }
}