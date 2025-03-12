using System.Collections.Generic;
using Tavern.Character;
using Tavern.Utils;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Tavern.Infrastructure
{
    public class TransformSerializer
    {
        private const string Position = "position";
        private const string Rotation = "rotation";
        private readonly ICharacter _character;

        public TransformSerializer(ICharacter character)
        {
            _character = character;
        }

        public string Serialize()
        {
            var info = new Dictionary<string, string>();
            Transform transform = _character.GetTransform();

            info[Position] = JsonConvert.SerializeObject(transform.position.ToFloat3());
            info[Rotation] = JsonConvert.SerializeObject(transform.rotation.ToFloat4());
            
            return JsonConvert.SerializeObject(info);
        }

        public void Deserialize(string value)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            if (data == null) return;

            Transform transform = _character.GetTransform();
            
            var position = JsonConvert.DeserializeObject<float[]>(data[Position]).ToVector3();
            transform.position = position;

            var rotation = JsonConvert.DeserializeObject<float[]>(data[Rotation]).ToQuaternion();
            transform.rotation = rotation;
        } 
    }
}