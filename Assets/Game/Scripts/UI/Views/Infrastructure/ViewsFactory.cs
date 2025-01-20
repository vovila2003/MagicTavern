using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class ViewsFactory : IViewsFactory
    {
        public IEntityCardViewPool EntityCardViewPool => _entityCardViewPool;
        public IItemCardViewPool ItemCardViewPool => _itemCardViewPool;
        private readonly UISettings _settings;
        private readonly Transform _canvasTransform;

        private readonly IEntityCardViewPool _entityCardViewPool;
        private readonly IItemCardViewPool _itemCardViewPool;

        public ViewsFactory(UISettings settings, UISceneSettings sceneSettings)
        {
            _settings = settings;
            _canvasTransform = sceneSettings.Canvas;
            _entityCardViewPool = new EntityCardViewPool(settings, sceneSettings.Pool);
            _itemCardViewPool = new ItemCardViewPool(settings, sceneSettings.Pool);
        }

        public IEntityCardView GetEntityCardView(Transform viewContentTransform)
        {
            if (_entityCardViewPool.TrySpawnEntityCardViewUnderTransform(
                    viewContentTransform,
                    out IEntityCardView view)) 
                return view;
            
            throw new System.Exception("Failed to get entity card view");
        }

        public IItemCardView GetItemCardView(Transform viewContentTransform)
        {
            if (_itemCardViewPool.TrySpawnItemCardViewUnderTransform(
                    viewContentTransform,
                    out IItemCardView view)) 
                return view;
            
            throw new System.Exception("Failed to get item card view");
        }
        
        public IPanelView CreatePanelView() => 
            Object.Instantiate(_settings.CommonSettings.Panel, _canvasTransform);

        public IContainerView CreateLeftGridView(Transform viewContainer) => 
            Object.Instantiate(_settings.CommonSettings.ContainerView, viewContainer);

        public ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.CookingMiniGameView, viewContainer);

        public IContainerView CreateCookingIngredientsView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.CookingIngredientsView, viewContainer);

        public IMatchRecipeView CreateMatchRecipeView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.MatchRecipeView, viewContainer);
        
        public IInfoPanelView CreateInfoPanelView() => 
            Object.Instantiate(_settings.CommonSettings.InfoPanel, _canvasTransform);
    }
}