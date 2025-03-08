using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class PauseView : View, IPauseView
    {
        public event UnityAction OnResume
        {
            add => ResumeButton.onClick.AddListener(value);
            remove => ResumeButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnExit
        {
            add => ExitButton.onClick.AddListener(value);
            remove => ExitButton.onClick.RemoveListener(value);
        }
        
        [SerializeField] 
        private Button ResumeButton;

        [SerializeField] 
        private Button ExitButton;
    }
}