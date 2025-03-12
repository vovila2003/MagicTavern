using System.Collections.Generic;
using Tavern.Utils;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Tavern.Infrastructure
{
    public class TransformSerializer
    {
        private const string Position = "position";
        private const string Rotation = "rotation";

        public string Serialize(Transform transform)
        {
            var info = new Dictionary<string, string>
            {
                [Position] = JsonConvert.SerializeObject(transform.position.ToFloat3()),
                [Rotation] = JsonConvert.SerializeObject(transform.rotation.ToFloat4())
            };

            return JsonConvert.SerializeObject(info);
        }

        public (Vector3, Quaternion) Deserialize(string value)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            if (data == null) return (Vector3.zero, Quaternion.identity);
            
            var position = JsonConvert.DeserializeObject<float[]>(data[Position]).ToVector3();
            var rotation = JsonConvert.DeserializeObject<float[]>(data[Rotation]).ToQuaternion();
            
            return (position, rotation);
        } 
    }
}