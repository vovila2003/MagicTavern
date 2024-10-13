using Character;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => FirePointProxy.FirePoint.position;

        public Quaternion Rotation => FirePointProxy.FirePoint.rotation;

        [SerializeField]
        private FirePointProxy FirePointProxy;
    }
}