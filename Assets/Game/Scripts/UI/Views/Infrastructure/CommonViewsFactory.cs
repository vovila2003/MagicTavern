using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class CommonViewsFactory : ICommonViewsFactory
    {
        private readonly UISettings _uiSettings;
        private readonly UISceneSettings _uiSceneSettings;
        
        public IEntityCardViewPool EntityCardViewPool { get; }
        public IItemCardViewPool ItemCardViewPool { get; }
        public IInfoViewProvider InfoViewProvider { get; }

        public CommonViewsFactory(GameSettings gameSettings, SceneSettings sceneSettings)
        {
            _uiSettings = gameSettings.UISettings;
            _uiSceneSettings = sceneSettings.UISceneSettings;
            
            EntityCardViewPool = new EntityCardViewPool(_uiSettings, _uiSceneSettings.Pool);
            ItemCardViewPool = new ItemCardViewPool(_uiSettings, _uiSceneSettings.Pool);
            InfoViewProvider = new InfoViewProvider(CreateInfoPanelView, _uiSceneSettings.Pool);
        }

        public IEntityCardView GetEntityCardView(Transform viewContentTransform)
        {
            if (EntityCardViewPool.TrySpawnEntityCardViewUnderTransform(
                    viewContentTransform,
                    out IEntityCardView view)) 
                return view;
            
            throw new System.Exception("Failed to get entity card view");
        }

        public IItemCardView GetItemCardView(Transform viewContentTransform)
        {
            if (ItemCardViewPool.TrySpawnItemCardViewUnderTransform(
                    viewContentTransform,
                    out IItemCardView view)) 
                return view;
            
            throw new System.Exception("Failed to get item card view");
        }
        
        public IPanelView CreatePanelView() => 
            Object.Instantiate(_uiSettings.CommonSettings.Panel, _uiSceneSettings.Canvas);
        
        public IPanelView CreateSmallPanelView() => 
            Object.Instantiate(_uiSettings.CommonSettings.SmallPanel, _uiSceneSettings.Canvas);

        public IContainerView CreateLeftGridView(Transform viewContainer) => 
            Object.Instantiate(_uiSettings.CommonSettings.ContainerView, viewContainer);
        
        public IEffectView CreateEffectView(Transform viewContainer) =>
            Object.Instantiate(_uiSettings.CommonSettings.EffectView, viewContainer);

        //private
        private InfoPanelView CreateInfoPanelView() =>
            Object.Instantiate(_uiSettings.CommonSettings.InfoPanel, _uiSceneSettings.Pool);
    }
}