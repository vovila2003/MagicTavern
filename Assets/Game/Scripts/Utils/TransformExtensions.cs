using UnityEngine;

namespace Tavern.Utils
{
    public static class TransformExtensions
    {
        public static float[] ToFloat3(this Vector3 position) => 
            new[] {position.x, position.y, position.z};

        public static Vector3 ToVector3(this float[] value) => 
            new(value[0], value[1], value[2]);

        public static float[] ToFloat4(this Quaternion quaternion) => 
            new[] {quaternion.x, quaternion.y, quaternion.z, quaternion.w};

        public static Quaternion ToQuaternion(this float[] value) => 
            new(value[0], value[1], value[2], value[3]);
    }
}