using System;
using Tavern.InputServices.Interfaces;
using TMPro;
using UnityEngine;
using VContainer;

namespace Tavern.Common
{
    public class Interactor : MonoBehaviour
    {
        public event Action OnActivated;
        
        [SerializeField] 
        private TMP_Text UiText;

        [SerializeField] 
        private GameObject Canvas;
        
        private IActionInput _input;
        
        [Inject]
        private void Construct(IActionInput input)
        {
            _input = input;
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
        
        private void OnDestroy()
        {
            _input.OnAction -= OnAction;
        }
        
        private void OnAction()
        {
            OnActivated?.Invoke();
        }
    }
}