using System;
using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.Common;
using UnityEngine;
using VContainer;

namespace Tavern.Shopping
{
    public class Shop : MonoBehaviour
    {
        public event Action OnActivated;
        
        [SerializeField]
        private Interactor Interactor;
        
        [SerializeField]
        private SpriteRenderer SpriteRenderer;
        
        [ShowInInspector, ReadOnly]
        private NpcSeller _npcSeller;

        [ShowInInspector, ReadOnly]
        private CharacterSeller _characterSeller;
        
        private Buyer _buyer;
        
        public SellerConfig SellerConfig { get; private set; }

        [Inject]
        private void Construct(Buyer buyer, CharacterSeller characterSeller)
        {
            _buyer = buyer;
            _characterSeller = characterSeller;
        }

        private void OnDisable()
        {
            Interactor.OnActivated -= OnAction;
        }
        
        public void Setup(SellerConfig pointConfig)
        {
            SellerConfig = pointConfig;
            _npcSeller = SellerConfig.Create();
            SpriteRenderer.sprite = SellerConfig.ShopMetadata.Icon;
            Interactor.OnActivated += OnAction;
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

        private void OnAction()
        {
            OnActivated?.Invoke();
        }
    }
}