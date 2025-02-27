using UnityEngine;

namespace Tavern.Cooking
{
    public class KitchenItemPoint : MonoBehaviour
    {
        [SerializeField] 
        private KitchenItemConfig KitchenItemConfig;

        public KitchenItemConfig Config => KitchenItemConfig;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 0.8f);
        }
    }
}