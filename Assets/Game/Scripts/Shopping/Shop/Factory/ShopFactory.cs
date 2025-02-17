using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Infrastructure;
using Tavern.Settings;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Shopping
{
    [UsedImplicitly]
    public class ShopFactory : IInitGameListener, IExitGameListener
    {
        private readonly IObjectResolver _resolver;
        private readonly List<ShopListener> _listeners = new();

        public ShopFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        void IInitGameListener.OnInit()
        {
            var gameCycleController = _resolver.Resolve<GameCycleController>();
            var uiManager = _resolver.Resolve<UiManager>();
            var sceneSettings = _resolver.Resolve<SceneSettings>();
            Shop prefab = _resolver.Resolve<ShoppingSettings>().ShopPrefab;
            
            foreach (ShopPoint point in sceneSettings.ShopPoints)
            {
                Shop shop = _resolver.Instantiate(prefab, point.transform.position,
                    point.transform.rotation, sceneSettings.ShopsParent);
                
                shop.Setup(point.Config);
            
                _listeners.Add(new ShopListener(shop, gameCycleController, uiManager));
            }
            
            foreach (ShopPoint point in sceneSettings.ShopPoints)
            {
                Object.Destroy(point.gameObject);
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (ShopListener listener in _listeners)
            {
                listener.Dispose();
            }
            
            _listeners.Clear();
        }
    }
}