using Tavern.Character.Visual;
using UnityEngine;

namespace Tavern.Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => FirePointProxy.FirePoint.position;

        public Quaternion Rotation => FirePointProxy.FirePoint.rotation;

        [SerializeField]
        private FirePointProxy FirePointProxy;
    }
}