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

        private KitchenItemConfig _kitchenItemConfig;

        private void OnDisable()
        {
            Interactor.OnActivated -= OnAction;
        }

        public void Setup(KitchenItemConfig config)
        {
            _kitchenItemConfig = config;
            SpriteRenderer.sprite = config.Metadata.Icon;
            Interactor.OnActivated += OnAction;
        }

        private void OnAction()
        {
            OnActivated?.Invoke(_kitchenItemConfig);
        }
    }
}