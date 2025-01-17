using System;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class UISceneSettings
    {
        [SerializeField] 
        private Transform CanvasTransform;
        
        [SerializeField] 
        private Transform EntityCardPoolTransform;
        
        [SerializeField] 
        private Transform ItemCardPoolTransform;
        
        [SerializeField] 
        private MainMenuView MainMenuView;
        
        [SerializeField] 
        private PauseView PauseView;

        [SerializeField] 
        private HudView HudView;

        public Transform Canvas => CanvasTransform;
        public Transform EntityCardTransform => EntityCardPoolTransform;
        public MainMenuView MainMenu => MainMenuView; 
        public PauseView Pause => PauseView; 
        public HudView Hud => HudView;
        public Transform ItemCardTransform => ItemCardPoolTransform;
    }
}