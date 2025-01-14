using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class PauseView : MonoBehaviour
    {
        public event UnityAction OnResume
        {
            add => ResumeButton.onClick.AddListener(value);
            remove => ResumeButton.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private Button ResumeButton;
    }
}