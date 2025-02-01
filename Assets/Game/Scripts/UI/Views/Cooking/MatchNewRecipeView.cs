using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public sealed class MatchNewRecipeView : View, IMatchNewRecipeView
    {
        public event UnityAction OnPressed
        {
            add => Button.onClick.AddListener(value);
            remove => Button.onClick.RemoveListener(value);
        }
        
        [SerializeField]
        private Button Button;
    }
}