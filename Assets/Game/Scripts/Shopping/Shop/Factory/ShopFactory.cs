using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using Tavern.Infrastructure;
using Tavern.Settings;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Tavern.Shopping
{
    [UsedImplicitly]
    public class ShopFactory : 
        IInitGameListener, 
        IExitGameListener
    {
        private readonly IObjectResolver _resolver;
        private readonly Dictionary<ShopContext, ShopListener> _listeners = new();
        private GameCycle _gameCycle;
        private IUiManager _uiManager;
        private SceneSettings _sceneSettings;
        private ShopContext _prefab;
        private CharacterBuyer _characterBuyer;
        private CharacterSeller _characterSeller;
        private TimeGameCycle _timeGameCycle;
        
        public IReadOnlyDictionary<ShopContext, ShopListener> Shops => _listeners;

        public ShopFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public void Clear()
        {
            foreach ((ShopContext context, ShopListener listener) in _listeners)
            {
                listener?.Dispose();
                try
                {
                    _timeGameCycle.RemoveListener(context.Shop);
                    Object.Destroy(context?.gameObject);
                }
                catch (Exception _)
                {
                    // ignored
                }
            }
            
            _listeners.Clear();
        }

        public void Create(Vector3 position, Quaternion rotation, SellerConfig config)
        {
            ShopContext shopContext = _resolver.Instantiate(_prefab, position, rotation, _sceneSettings.ShopsParent);

            var shop = new Shop(_characterSeller, _characterBuyer, config); 
            shopContext.Setup(shop);
            _timeGameCycle.AddListener(shop);
            
            _listeners.Add(shopContext, new ShopListener(shopContext, _gameCycle, _uiManager));
        }

        void IInitGameListener.OnInit()
        {
            InitFields();
            foreach (ShopPoint point in _sceneSettings.ShopPoints)
            {
                Create(point.transform.position, point.transform.rotation, point.Config);
            }
            
            foreach (ShopPoint point in _sceneSettings.ShopPoints)
            {
                Object.Destroy(point.gameObject);
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach ((ShopContext shopContext, ShopListener listener) in _listeners)
            {
                listener.Dispose();
                _timeGameCycle.RemoveListener(shopContext.Shop);
            }
            
            _listeners.Clear();
        }

        private void InitFields()
        {
            _gameCycle ??= _resolver.Resolve<GameCycle>();
            _uiManager ??= _resolver.Resolve<IUiManager>();
            _sceneSettings ??= _resolver.Resolve<SceneSettings>();
            _prefab ??= _resolver.Resolve<GameSettings>().ShoppingSettings.ShopContextPrefab;
            _characterBuyer ??= _resolver.Resolve<CharacterBuyer>();
            _characterSeller ??= _resolver.Resolve<CharacterSeller>();
            _timeGameCycle ??= _resolver.Resolve<TimeGameCycle>();
        }
    }
}