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
    public class ShopFactory : 
        IInitGameListener, 
        IExitGameListener
    {
        private readonly IObjectResolver _resolver;
        private readonly List<ShopListener> _listeners = new();
        private readonly GameCycleController _gameCycleController;
        private readonly IUiManager _uiManager;
        private readonly SceneSettings _sceneSettings;
        private readonly ShopContext _prefab;
        private readonly CharacterBuyer _characterBuyer;
        private readonly CharacterSeller _characterSeller;

        public ShopFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
            _gameCycleController = _resolver.Resolve<GameCycleController>();
            _uiManager = _resolver.Resolve<IUiManager>();
            _sceneSettings = _resolver.Resolve<SceneSettings>();
            _prefab = _resolver.Resolve<GameSettings>().ShoppingSettings.ShopContextPrefab;
            _characterBuyer = _resolver.Resolve<CharacterBuyer>();
            _characterSeller = _resolver.Resolve<CharacterSeller>();
        }

        void IInitGameListener.OnInit()
        {
            
            foreach (ShopPoint point in _sceneSettings.ShopPoints)
            {
                ShopContext shopContext = _resolver.Instantiate(_prefab, point.transform.position,
                    point.transform.rotation, _sceneSettings.ShopsParent);

                var shop = new Shop(_characterSeller, _characterBuyer, point.Config); 
                shopContext.Setup(shop);
            
                _listeners.Add(new ShopListener(shopContext, _gameCycleController, _uiManager));
            }
            
            foreach (ShopPoint point in _sceneSettings.ShopPoints)
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