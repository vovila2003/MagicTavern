using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Character;
using Tavern.Settings;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class CharacterSerializer : IGameSerializer
    {
        [Serializable]
        public class StateData
        {
            public string EffectName;
        }
        
        [Serializable]
        public class CharacterData
        {
            public int Hp;
            public List<StateData> State;
            public float[] Position;
            public float[] Rotation;
            public float Speed;
        }
        
        private const string Character = "Character";
        
        private readonly CharacterStateSerializer _characterStateSerializer;
        private readonly ICharacter _character;

        public CharacterSerializer(ICharacter character, GameSettings settings)
        {
            _character = character;
            _characterStateSerializer =
                new CharacterStateSerializer(character, settings.EffectsSettings.EffectsCatalog);
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            Transform transform = _character.GetTransform();
            var info = new CharacterData
            {
                Hp = _character.GetHpComponent().CurrentHp,
                Position = transform.position.ToFloat3(),
                Rotation = transform.rotation.ToFloat4(),
                Speed = _character.GetMoveComponent().Speedable.GetSpeed(),
                State = _characterStateSerializer.Serialize()
            };

            saveState[Character] = Serializer.SerializeObject(info);
        }
        
        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(Character, out string json)) return;
        
            (CharacterData info, bool ok) = Serializer.DeserializeObject<CharacterData>(json);
            if (!ok) return;
            
            _character.GetHpComponent().Set(info.Hp);
            _characterStateSerializer.Deserialize(info.State);
            
            Transform transform = _character.GetTransform();
            transform.position = info.Position.ToVector3();
            transform.rotation = info.Rotation.ToQuaternion();       
            _character.GetMoveComponent().Speedable.SetSpeed(info.Speed);
        }
    }
}