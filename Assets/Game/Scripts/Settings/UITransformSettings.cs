using System;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class UITransformSettings
    {
        [SerializeField] 
        private Transform CanvasTransform;
        
        [SerializeField] 
        private Transform EntityCardPoolParent;
        
        [SerializeField] 
        private MainMenuView MainMenuView;
        
        [SerializeField] 
        private PauseView PauseView;

        [SerializeField] 
        private HudView HudView;

        public Transform Canvas => CanvasTransform;
        public Transform EntityCardParent => EntityCardPoolParent;
        public MainMenuView MainMenu => MainMenuView; 
        public PauseView Pause => PauseView; 
        public HudView Hud => HudView; 
    }
}