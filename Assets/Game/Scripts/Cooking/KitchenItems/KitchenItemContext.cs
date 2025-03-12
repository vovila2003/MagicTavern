using System;
using Tavern.Common;
using UnityEngine;

namespace Tavern.Cooking
{
    public class KitchenItemContext : MonoBehaviour
    {
        public event Action<KitchenItemConfig> OnActivated;
        
        [SerializeField]
        private Interactor Interactor;
        
        [SerializeField]
        private SpriteRenderer SpriteRenderer;

        public KitchenItemConfig KitchenItemConfig { get; private set; }

        private void OnDisable()
        {
            Interactor.OnActivated -= OnAction;
        }

        public void Setup(KitchenItemConfig config)
        {
            KitchenItemConfig = config;
            SpriteRenderer.sprite = config.Metadata.Icon;
            Interactor.OnActivated += OnAction;
        }

        private void OnAction()
        {
            OnActivated?.Invoke(KitchenItemConfig);
        }
    }
}