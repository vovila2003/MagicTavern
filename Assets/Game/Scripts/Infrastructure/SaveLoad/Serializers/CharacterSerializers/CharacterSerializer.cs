using JetBrains.Annotations;
using Tavern.Character;
using Tavern.Settings;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class CharacterSerializer : GameSerializer<CharacterData>
    {
        private readonly CharacterStateSerializer _characterStateSerializer;
        private readonly ICharacter _character;

        public CharacterSerializer(ICharacter character, GameSettings settings)
        {
            _character = character;
            _characterStateSerializer =
                new CharacterStateSerializer(character, settings.EffectsSettings.EffectsCatalog);
        }
        
        protected override CharacterData Serialize()
        {
            Transform transform = _character.GetTransform();
            return new CharacterData
            {
                Hp = _character.GetHpComponent().CurrentHp,
                Position = transform.position.ToFloat3(),
                Rotation = transform.rotation.ToFloat4(),
                Speed = _character.GetMoveComponent().Speedable.GetSpeed(),
                State = _characterStateSerializer.Serialize()
            };
        }

        protected override void Deserialize(CharacterData data)
        {
            _character.GetHpComponent().Set(data.Hp);
            _characterStateSerializer.Deserialize(data.State);
            
            _character.SetPosition(data.Position.ToVector3());
            _character.SetRotation(data.Rotation.ToQuaternion());
            _character.GetMoveComponent().Speedable.SetSpeed(data.Speed);
        }
    }
}