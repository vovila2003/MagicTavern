using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class MainMenuView : MonoBehaviour
    {
        public event UnityAction OnStartGame
        {
            add => StartButton.onClick.AddListener(value);
            remove => StartButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnQuitGame
        {
            add => QuitButton.onClick.AddListener(value);
            remove => QuitButton.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private Button StartButton;
        
        [SerializeField]
        private Button QuitButton;
    }
}