using System;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class UISettings
    {
        [SerializeField] 
        private EntityCardSettings EntityCardSettings;
        
        [SerializeField]
        private LeftGridView LeftGridPrefab;
        
        [SerializeField]
        private CookingView CookingPanelPrefab;

        public EntityCardSettings EntityCardConfig => EntityCardSettings;
        public LeftGridView LeftGridView => LeftGridPrefab;
        public CookingView CookingPanel => CookingPanelPrefab;
    }
}