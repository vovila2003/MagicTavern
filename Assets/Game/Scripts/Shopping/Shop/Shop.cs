using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Shopping.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField]
        private SellerConfig SellerConfig;

        [ShowInInspector, ReadOnly]
        private Seller _seller;

        private void Awake()
        {
            _seller = new Seller(SellerConfig);
        }

        [Button]
        public void WeeklyUpdate()
        {
            _seller?.WeeklyUpdate();
        }

    }
}