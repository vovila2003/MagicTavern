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
            _entityCardViewPool = new EntityCardViewPool(settings, sceneSettings.EntityCardTransform);
            _itemCardViewPool = new ItemCardViewPool(settings, sceneSettings.ItemCardTransform);
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

        public IPanelView CreatePanelView()
        {
            return Object.Instantiate(_settings.Panel, _canvasTransform);
        }

        public IContainerView CreateLeftGridView(Transform viewContainer)
        {
            return Object.Instantiate(_settings.ContainerView, viewContainer);
        }

        public ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer)
        {
            return Object.Instantiate(_settings.CookingMiniGameView, viewContainer);
        }
        
        public IContainerView CreateCookingIngredientsView(Transform viewContainer)
        {
            return Object.Instantiate(_settings.CookingIngredientsView, viewContainer);
        }
    }
}