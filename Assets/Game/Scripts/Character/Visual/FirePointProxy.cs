using Sirenix.OdinInspector;
using UnityEngine;

namespace Character
{
    public sealed class FirePointProxy : MonoBehaviour
    {
        public Transform FirePoint => _firePoint;
        
        [SerializeField]
        private string FirePointName = "FirePoint";

        [SerializeField] 
        private Transform View;

        [ShowInInspector, ReadOnly]
        private Transform _firePoint;

        private void Start()
        {
            _firePoint = FindChildByPath(View, FirePointName);
        }

        private static Transform FindChildByPath(Transform parent, string path)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.name.Equals(path))
                {
                    return child;
                }
            }
            return null;
        }
    }
}