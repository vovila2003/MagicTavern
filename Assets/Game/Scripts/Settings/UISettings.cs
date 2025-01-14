using System;
using Tavern.UI;
using Tavern.UI.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tavern.Settings
{
    [Serializable]
    public class UISettings
    {
        [SerializeField]    
        private EntityCardView EntityCardPrefab;
        
        [SerializeField]
        private LeftGridView LeftGridPrefab;
        
        [SerializeField]
        private Transform CanvasTransform;

        public EntityCardView EntityCard => EntityCardPrefab;
        public LeftGridView LeftGridView => LeftGridPrefab;
        public Transform Canvas => CanvasTransform;
    }
}