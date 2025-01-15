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

        public EntityCardSettings EntityCardConfig => EntityCardSettings;
        public LeftGridView LeftGridView => LeftGridPrefab;
    }
}