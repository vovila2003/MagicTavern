using UnityEngine;

namespace Tavern.Utils
{
    public static class TransformExtensions
    {
        public static float[] ToFloat3(this Vector3 position)
        {
            return new[]{position.x, position.y, position.z};
        }

        public static Vector3 ToVector3(this float[] value)
        {
            return new Vector3(value[0], value[1], value[2]);
        }

        public static float[] ToFloat4(this Quaternion quaternion)
        {
            return new[] {quaternion.x, quaternion.y, quaternion.z, quaternion.w};
        }
        
        public static Quaternion ToQuaternion(this float[] value)
        {
            return new Quaternion(value[0], value[1], value[2], value[3]);
        }
    }
}