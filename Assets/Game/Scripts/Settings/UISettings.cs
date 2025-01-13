using System;
using Tavern.UI;
using UnityEngine;

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