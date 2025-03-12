using System.Collections.Generic;
using Tavern.Utils;
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
                [Position] = Serializer.SerializeObject(transform.position.ToFloat3()),
                [Rotation] = Serializer.SerializeObject(transform.rotation.ToFloat4())
            };

            return Serializer.SerializeObject(info);
        }

        public (Vector3, Quaternion) Deserialize(string value)
        {
            var data = Serializer.DeserializeObject<Dictionary<string, string>>(value);
            if (data == null) return (Vector3.zero, Quaternion.identity);
            
            var position = Serializer.DeserializeObject<float[]>(data[Position]).ToVector3();
            var rotation = Serializer.DeserializeObject<float[]>(data[Rotation]).ToQuaternion();
            
            return (position, rotation);
        } 
    }
}