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
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}