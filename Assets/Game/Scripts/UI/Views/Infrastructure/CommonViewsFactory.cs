using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class CommonViewsFactory : ICommonViewsFactory
    {
        public IEntityCardViewPool EntityCardViewPool { get; }
        public IItemCardViewPool ItemCardViewPool { get; }
        public IInfoViewProvider InfoViewProvider { get; }

        private readonly UISettings _settings;
        private readonly UISceneSettings _sceneSettings;

        public CommonViewsFactory(UISettings settings, UISceneSettings sceneSettings)
        {
            _settings = settings;
            _sceneSettings = sceneSettings;
            
            EntityCardViewPool = new EntityCardViewPool(settings, _sceneSettings.Pool);
            ItemCardViewPool = new ItemCardViewPool(settings, _sceneSettings.Pool);
            InfoViewProvider = new InfoViewProvider(CreateInfoPanelView, _sceneSettings.Pool);
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
            Object.Instantiate(_settings.CommonSettings.Panel, _sceneSettings.Canvas);

        public IContainerView CreateLeftGridView(Transform viewContainer) => 
            Object.Instantiate(_settings.CommonSettings.ContainerView, viewContainer);

        //private
        private InfoPanelView CreateInfoPanelView() =>
            Object.Instantiate(_settings.CommonSettings.InfoPanel, _sceneSettings.Pool);
    }
}