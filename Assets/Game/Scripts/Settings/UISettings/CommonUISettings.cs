using System;
using Tavern.UI.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tavern.Settings
{
    [Serializable]
    public class CommonUISettings
    {
        [SerializeField]
        private EntityCardSettings EntityCardSettings;

        [SerializeField]
        private ItemCardSettings ItemCardSettings;

        [SerializeField]
        private PanelView PanelPrefab;

        [SerializeField]
        private ContainerView LeftGridPrefab;
        
        [SerializeField]
        private InfoPanelView InfoPanelPrefab;
                
        public EntityCardSettings EntityCardConfig => EntityCardSettings;
        public ItemCardSettings ItemCardConfig => ItemCardSettings;
        public ContainerView ContainerView => LeftGridPrefab;
        public PanelView Panel => PanelPrefab;
        public InfoPanelView InfoPanel => InfoPanelPrefab;
    }
}