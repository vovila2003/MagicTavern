using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public class LeftGridView : View, ILeftGridView
    {
        public Transform ContentTransform => Content;

        [SerializeField] 
        private Transform Content;
    }
}