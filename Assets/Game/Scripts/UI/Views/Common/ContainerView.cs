using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public class ContainerView : View, IContainerView
    {
        public Transform ContentTransform => Content;

        [SerializeField] 
        private Transform Content;
    }
}