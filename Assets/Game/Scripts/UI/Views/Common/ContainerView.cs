using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class ContainerView : View, IContainerView
    {
        [SerializeField]
        private ScrollRect ScrollRect; 
        
        [SerializeField] 
        private Transform Content;
        
        public Transform ContentTransform => Content;
        
        public void Up()
        {
            ScrollRect.verticalNormalizedPosition = 1;    
        }
    }
}