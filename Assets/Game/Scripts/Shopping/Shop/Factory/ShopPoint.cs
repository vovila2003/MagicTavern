using UnityEngine;

namespace Tavern.Shopping
{
    public class ShopPoint : MonoBehaviour
    {
        [SerializeField] 
        private SellerConfig SellerConfig;

        public SellerConfig Config => SellerConfig;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.8f);
            Gizmos.color = Color.white;
        }
    }
}