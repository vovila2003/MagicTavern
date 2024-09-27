using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => FirePoint.position;

        public Quaternion Rotation => FirePoint.rotation;

        [SerializeField]
        private Transform FirePoint;
    }
}