using System;
using Modules.Items;
using Sirenix.OdinInspector;
using Tavern.Common;
using UnityEngine;

namespace Tavern.Shopping
{
    public class ShopContext : MonoBehaviour
    {
        public event Action OnActivated;
        
        [SerializeField]
        private Interactor Interactor;
        
        [SerializeField]
        private SpriteRenderer SpriteRenderer;

        private Shop _shop;

        public SellerConfig SellerConfig => _shop.SellerConfig;
        
        [ShowInInspector, ReadOnly]
        public Shop Shop => _shop;

        private void OnDisable()
        {
            Interactor.OnActivated -= OnAction;
            _shop.Dispose();
        }
        
        public void Setup(Shop shop)
        {
            _shop = shop;
            
            SpriteRenderer.sprite = SellerConfig.ShopMetadata.Icon;
            Interactor.OnActivated += OnAction;
        }

        [Button]
        public void WeeklyUpdate() => _shop.WeeklyUpdate();

        [Button]
        public void BuyByConfig(ItemConfig itemConfig) => _shop.BuyByConfig(itemConfig);

        public void BuyOut(Item item) => _shop.BuyOut(item);

        public void Sell(Item item) => _shop.Sell(item);

        [Button]
        public void SetReputation(int reputation) => _shop.SetReputation(reputation);

        private void OnAction()
        {
            OnActivated?.Invoke();
        }
    }
}