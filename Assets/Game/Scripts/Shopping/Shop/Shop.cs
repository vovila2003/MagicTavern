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
        
        [ShowInInspector, ReadOnly]
        private CharacterSeller _characterSeller;

        [Inject]
        private void Construct(Buyer buyer, CharacterSeller characterSeller)
        {
            _buyer = buyer;
            _characterSeller = characterSeller;
        }

        private void Awake()
        {
            _npcSeller = SellerConfig.Create();
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

            bool result = Deal.BuyFromNpc(_buyer, _npcSeller, itemConfig, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        public void Sell(Item item)
        {
            if (!_characterSeller.HasItem(item))
            {
                Debug.Log($"Character doesn't have item {item.ItemName}");
                return;
            }
            
            (bool hasPrice, int price) = _characterSeller.GetItemPrice(item);
            
            if (!hasPrice)
            {
                Debug.Log($"Can't sell item {item.ItemName}. Unknown price.");
                return;
            }
            
            bool result = Deal.SellToNpc(_npcSeller, _characterSeller, item, price);
            string dealResult = result ? "OK" : "FAIL";
            Debug.Log($"Deal result: {dealResult}");
        }

        [Button]
        public void SetReputation(int reputation)
        {
            _npcSeller.UpdateReputation(reputation);
        }
    }
}