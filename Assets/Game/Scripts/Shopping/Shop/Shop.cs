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
        private NpcSeller _npcSeller;

        private Buyer _buyer;

        [Inject]
        private void Construct(Buyer buyer)
        {
            _buyer = buyer;
        }

        private void Awake()
        {
            _npcSeller = new NpcSeller(SellerConfig);
        }

        [Button]
        public void WeeklyUpdate()
        {
            _npcSeller?.WeeklyUpdate();
        }

        [Button]
        public void Buy(ItemConfig itemConfig)
        {
            if (!_npcSeller.HasItem(itemConfig))
            {
                Debug.Log($"Shop doesn't have item {itemConfig.Name}");
                return;
            }
            
            (bool hasPrice, int price) = _npcSeller.GetItemPrice(itemConfig);

            if (!hasPrice)
            {
                Debug.Log($"Can't sell item {itemConfig.Name}. Unknown price.");
                return;
            }

            bool result = Deal.SellFromNpc(_buyer, _npcSeller, itemConfig, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }
    }
}