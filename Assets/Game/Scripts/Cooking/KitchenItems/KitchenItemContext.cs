using System;
using Tavern.InputServices.Interfaces;
using TMPro;
using UnityEngine;
using VContainer;

namespace Tavern.Cooking
{
    public class KitchenItemContext : MonoBehaviour
    {
        public event Action<KitchenItemConfig> OnActivated;
        
        [SerializeField] 
        private TMP_Text UiText;

        [SerializeField] 
        private GameObject Canvas;
        
        [SerializeField]
        private SpriteRenderer SpriteRenderer;

        private IActionInput _input;
        private KitchenItemConfig _kitchenItemConfig;

        [Inject]
        private void Construct(IActionInput input)
        {
            _input = input;
        }

        public void Setup(KitchenItemConfig config)
        {
            _kitchenItemConfig = config;
            SpriteRenderer.sprite = config.Metadata.Icon;
        }

        private void OnDestroy()
        {
            _input.OnAction -= OnAction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _input.OnAction += OnAction;
            
            Canvas.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _input.OnAction -= OnAction;
            
            Canvas.SetActive(false);
        }

        private void OnAction()
        {
            OnActivated?.Invoke(_kitchenItemConfig);
        }
    }
}