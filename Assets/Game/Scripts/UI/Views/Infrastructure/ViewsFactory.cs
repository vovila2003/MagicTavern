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

        private readonly UISettings _settings;
        private readonly Transform _canvasTransform;

        private readonly EntityCardViewPool _entityCardViewPool;

        public ViewsFactory(UISettings settings, UISceneSettings sceneSettings)
        {
            _settings = settings;
            _canvasTransform = sceneSettings.Canvas;
            _entityCardViewPool = new EntityCardViewPool(settings, sceneSettings.EntityCardTransform);
        }

        public IEntityCardView GetEntityCardView()
        {
            if (_entityCardViewPool.TrySpawnEntityCardView(out IEntityCardView view))
            {
                return view;
            }
            
            throw new System.Exception("Failed to get entity card view");
        }

        public ILeftGridView CreateLeftGridView(Transform viewContainer)
        {
            return Object.Instantiate(_settings.LeftGridView, viewContainer);
        }

        public ICookingView CreateCookingPanelView()
        {
            return Object.Instantiate(_settings.CookingPanel, _canvasTransform);
        }
    }
}