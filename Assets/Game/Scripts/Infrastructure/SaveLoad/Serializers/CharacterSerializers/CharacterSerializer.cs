using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Character;
using Tavern.Settings;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class CharacterSerializer : IGameSerializer
    {
        private const string Hp = "Hp";
        private const string State = "State";
        private const string Transform = "Transform";
        private const string Speed = "Speed";
        private const string Character = "Character";
        
        private readonly HitPointSerializer _hitPointSerializer;
        private readonly CharacterStateSerializer _characterStateSerializer;
        private readonly TransformSerializer _transformSerializer;
        private readonly SpeedSerializer _speedSerializer;
        private readonly ICharacter _character;

        public CharacterSerializer(ICharacter character, GameSettings settings)
        {
            _character = character;
            _hitPointSerializer = new HitPointSerializer(character);
            _characterStateSerializer =
                new CharacterStateSerializer(character, settings.EffectsSettings.EffectsCatalog);
            _transformSerializer = new TransformSerializer();
            _speedSerializer = new SpeedSerializer(character);
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var info = new Dictionary<string, string>
            {
                [Hp] = _hitPointSerializer.Serialize(),
                [State] = _characterStateSerializer.Serialize(),
                [Transform] = _transformSerializer.Serialize(_character.GetTransform()),
                [Speed] = _speedSerializer.Serialize()
            };

            saveState[Character] = JsonConvert.SerializeObject(info);
        }
        
        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(Character, out string json)) return;

            var info = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (info == null) return;

            if (info.TryGetValue(Hp, out string hpString))
            {
                _hitPointSerializer.Deserialize(hpString);
            }
            
            if (info.TryGetValue(State, out string stateString))
            {
                _characterStateSerializer.Deserialize(stateString);
            }
            
            if (info.TryGetValue(Transform, out string transformString))
            {
                Transform transform = _character.GetTransform();
                (Vector3 position, Quaternion rotation) = _transformSerializer.Deserialize(transformString);
                transform.position = position;
                transform.rotation = rotation;
            }
            
            if (info.TryGetValue(Speed, out string speedString))
            {
                _speedSerializer.Deserialize(speedString);
            }
        }
    }
}