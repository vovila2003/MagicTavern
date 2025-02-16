using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.Shopping.Buying;
using UnityEngine;
using VContainer;

namespace Tavern.Shopping.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField]
        private SellerConfig SellerConfig;

        [ShowInInspector, ReadOnly]
        private Seller _seller;

        private Buyer _buyer;

        [Inject]
        private void Construct(Buyer buyer)
        {
            _buyer = buyer;
        }

        private void Awake()
        {
            _seller = new Seller(SellerConfig);
        }

        [Button]
        public void WeeklyUpdate()
        {
            _seller?.WeeklyUpdate();
        }

        [Button]
        public void Buy(ItemConfig itemConfig)
        {
            if (!_seller.HasItem(itemConfig))
            {
                Debug.Log($"Shop doesn't have item {itemConfig.Name}");
                return;
            }
            
            (bool hasPrice, int price) = _seller.GetItemPrice(itemConfig);

            if (!hasPrice)
            {
                Debug.Log($"Can't sell item {itemConfig.Name}. Unknown price.");
                return;
            }

            bool result = Deal.Make(_buyer, _seller, itemConfig, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }
    }
}